using AutoMapper;
using HDNXUdemyData.Entities;
using HDNXUdemyData.IRepository;
using HDNXUdemyModel.Base;
using HDNXUdemyModel.Constant;
using HDNXUdemyModel.Model;
using HDNXUdemyModel.ResponModel;
using HDNXUdemyModel.SystemExceptions;
using HDNXUdemyServices.CommonFunction;
using HDNXUdemyServices.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using NodaTime;
using Stripe;
using Stripe.Checkout;

namespace HDNXUdemyServices.Services
{
    public class PurcharseCourseServices : IPurcharseCourseServices
    {
        private readonly IPurcharseCourseRepository _purcharseCourseRepository;
        private readonly IRPPurcharseCourseDetailsRepository _purcharseCourseDetailsRepository;
        private readonly IMapper _mapper;
        private readonly IHubContext<HubConfigProject> _hubConfigProject;
        private readonly ICourseRepository _courseRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly IInformationManualBankingRepository _informationManualBankingRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPurcharseCourseRepository _pucharseCourseRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IDistributedCache _distributedCache;

        public PurcharseCourseServices(IPurcharseCourseRepository purcharseCourseRepository, IMapper mapper, IHubContext<HubConfigProject> hubConfigProject,
            ICourseRepository courseRepository, INotificationRepository notificationRepository, IInformationManualBankingRepository informationManualBankingRepository,
            IRPPurcharseCourseDetailsRepository purcharseCourseDetailsRepository, IHttpContextAccessor httpContextAccessor, IPurcharseCourseRepository pucharseCourseRepository,
            IUserRepository userRepository, ICategoryRepository categoryRepository, IDistributedCache distributedCache)
        {
            _purcharseCourseRepository = purcharseCourseRepository ?? throw new ProjectException(nameof(_purcharseCourseRepository));
            _mapper = mapper ?? throw new ProjectException(nameof(_mapper));
            _hubConfigProject = hubConfigProject ?? throw new ProjectException(nameof(_hubConfigProject));
            _courseRepository = courseRepository ?? throw new ProjectException(nameof(_courseRepository));
            _notificationRepository = notificationRepository ?? throw new ProjectException(nameof(_notificationRepository));
            _informationManualBankingRepository = informationManualBankingRepository ?? throw new ProjectException(nameof(_informationManualBankingRepository));
            _purcharseCourseDetailsRepository = purcharseCourseDetailsRepository ?? throw new ProjectException(nameof(_purcharseCourseDetailsRepository));
            _httpContextAccessor = httpContextAccessor ?? throw new ProjectException(nameof(_httpContextAccessor));
            _pucharseCourseRepository = pucharseCourseRepository ?? throw new ProjectException(nameof(_pucharseCourseRepository));
            _userRepository = userRepository ?? throw new ProjectException(nameof(_userRepository));
            _categoryRepository = categoryRepository ?? throw new ProjectException(nameof(_categoryRepository));
            _distributedCache = distributedCache ?? throw new ProjectException(nameof(_distributedCache));
        }

        public string GenPurchaseOrder(long idStudent)
        {
            return Guid.NewGuid().ToString();
        }

        public async Task<PurcharseCourseModel> CreateRequestPurchase(PurcharseCourseModel model)
        {
            var getValueOfInfoPurechase = _mapper.Map<InformationManualBankingModel>(
                (await _informationManualBankingRepository.GetAsync(x => x.Status == (int)EStatus.Active)).FirstOrDefault());

            var dataInsert = _mapper.Map<PurcharseCourseEntities>(model);
            var addReturnModel = await _purcharseCourseRepository.AddReturnModelAsync(dataInsert);

            model.ListPurchaseCourseDetails?.ForEach(item =>
            {
                item.IdPurchaseOrder = addReturnModel.Id;
                item.IdStudent = addReturnModel.IdStudent;
            });
            var dataInsertDetail = _mapper.Map<List<PurcharseCourseDetailsEntities>>(model.ListPurchaseCourseDetails);
            await _purcharseCourseDetailsRepository.AddManyAsync(dataInsertDetail);

            return model;
        }

        public async Task<bool> UpdateStatusPurchase(long id, PurcharseCourseModel model)
        {
            var getData = await _purcharseCourseRepository.GetByIdAsync(id) ?? new PurcharseCourseEntities();
            getData.Status = model.Status;
            getData.PurcharseStatus = model.PurcharseStatus;
            bool returnValue = await _purcharseCourseRepository.UpdateAsync(getData);
            var getDataOfCourse = await _courseRepository.GetObjectAsync(id);
            if (returnValue && getDataOfCourse != null)
            {
                var dataInsertNotification = new NotificationEntities()
                {
                    IdCourse = getDataOfCourse.Id,
                    ShortComment = $"Khoá học {getDataOfCourse?.Title} đã thanh toán thành công",
                    IdStudent = model.IdStudent,
                    IsRead = false,
                    TypeNotification = (int)TypeNotification.UpdateOnCourse,
                };

                var insertNotification = await _notificationRepository.AddReturnModelAsync(dataInsertNotification);
                var contentNotification = new InfoNotification()
                {
                    IdNotification = insertNotification.Id.ToString(),
                    IsRead = insertNotification.IsRead,
                    NotificationContent = insertNotification.ShortComment,
                };
                await _hubConfigProject.Clients.Client(model.IdStudent.ToString())
                    .SendAsync(TypeNotification.UpdateOnCourse.GetEnumDescription(), contentNotification);
            }
            return returnValue;
        }

        public async Task<bool> IsCheckCoursePurchase(long idCourse)
        {
            long idCurrentUser = _httpContextAccessor.HttpContext == null ? 0 : long.Parse(_httpContextAccessor.HttpContext.User.Claims
                    .Where(x => x.Type == "user-id").FirstOrDefault()?.Value ?? "0");
            var isPurchaseCourseDetails = await _purcharseCourseDetailsRepository.GetAsync(x => x.IdCourse == idCourse && x.IdStudent == idCurrentUser);
            return isPurchaseCourseDetails.Any();
        }

        public async Task<PagedResult<PurcharseCourseModel>> GetListPurcharseCourses(int pageIndex, int pageSize)
        {
            DateTime firstDayOfCurrentMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime firstDayOfPreviousMonth = firstDayOfCurrentMonth.AddMonths(-1);
            DateTime currentDayOfCurrentMonth = DateTime.Now;
            DateTime currentDayOfPreviousMonth = currentDayOfCurrentMonth.AddMonths(-1);
            var getData = (await _pucharseCourseRepository.GetAsync(x => x.Status == (int)EStatus.Active))
                .OrderByDescending(x => x.CreateDate)
                .GetPagingPaged(pageIndex, pageSize, null);
            var returnValue = _mapper.Map<PagedResult<PurcharseCourseModel>>(getData);
            var getDataOfCurrentMonth = await _pucharseCourseRepository.GetAsync(x => x.CreateDate >= LocalDateTime.FromDateTime(firstDayOfCurrentMonth)
                            && x.CreateDate <= LocalDateTime.FromDateTime(currentDayOfCurrentMonth));
            var getDataOfPreviousMonth = await _pucharseCourseRepository.GetAsync(x => x.CreateDate >= LocalDateTime.FromDateTime(firstDayOfPreviousMonth)
                                        && x.CreateDate <= LocalDateTime.FromDateTime(currentDayOfPreviousMonth));
            foreach (var item in returnValue.Results)
            {
                item.User = _mapper.Map<UserModel>(await _userRepository.GetByIdAsync(item.IdStudent));
                item.NameStatus = ((ETypeOfStatusOrder)item.PurcharseStatus).GetEnumDescription();
            }

            if (returnValue.Results.Count != 0)
            {
                returnValue.Results[0].ValueOfDataCount = GetDataValueForStatusOfPurchaseOrder(getDataOfCurrentMonth.ToList(), getDataOfPreviousMonth.ToList());
                return returnValue;
            }

            return new PagedResult<PurcharseCourseModel>();
        }

        public async Task<PurcharseCourseModel> GetPurchaseCorseDetail(long idPurchase)
        {
            var getDataPurchaseOrder = await _pucharseCourseRepository.GetByIdAsync(idPurchase);
            var getDataPurchaseOrderDetail = await _purcharseCourseDetailsRepository.GetAsync(x => x.IdPurchaseOrder == idPurchase && x.Status == (int)EStatus.Active);

            var returnData = _mapper.Map<PurcharseCourseModel>(getDataPurchaseOrder);
            if (getDataPurchaseOrder != null)
            {
                var getDataStudent = await _userRepository.GetByIdAsync(getDataPurchaseOrder.IdStudent);
                var getCategory = await _categoryRepository.GetAllAsync();
                returnData.ListPurchaseCourseDetails = _mapper.Map<List<PurcharseCourseDetailsModel>>(getDataPurchaseOrderDetail);
                returnData.User = _mapper.Map<UserModel>(getDataStudent);
                returnData.NameStatus = ((ETypeOfStatusOrder)returnData.PurcharseStatus).GetEnumDescription();

                foreach (var item in returnData.ListPurchaseCourseDetails)
                {
                    item.Courses = _mapper.Map<CourseModel>(await _courseRepository.GetObjectAsync(x => x.Id == item.IdCourse && x.Status == (int)EStatus.Active));
                    item.Courses.CategoryName = getCategory.Where(x => x.Id == item.Courses.IdCategory).FirstOrDefault()?.Name;
                    item.Courses.AmountOfTheCourse = item.Courses.IsDiscount == true ? item.Courses.PriceOfDiscount : item.Courses.PriceOfCourse;
                }
            }
            return returnData;
        }

        private static List<ValuePurchaseOrderCount> GetDataValueForStatusOfPurchaseOrder(List<PurcharseCourseEntities> currentPurcharseCourseModels, List<PurcharseCourseEntities> sameWithCurrentPurcharseCourseModels)
        {
            var listValuePurchaseOrderStatus = new List<ValuePurchaseOrderCount>();
            var resultDataMapper = currentPurcharseCourseModels.GroupBy(x => x.PurcharseStatus);
            foreach (var item in resultDataMapper)
            {
                var valueOfSameWithPurchase = sameWithCurrentPurcharseCourseModels.Where(x => x.PurcharseStatus == item.Key);
                var dataReturn = new ValuePurchaseOrderCount()
                {
                    KeyValue = ((ETypeOfStatusOrder)item.Key).GetEnumDescription(),
                    PercentBasePreviewMonth = valueOfSameWithPurchase.Sum(x => x.TotalPrice) > 0 ? item.Sum(x => x.TotalPrice) ?? 0 / valueOfSameWithPurchase.Sum(x => x.TotalPrice) : 0,
                    ValueOfCurrent = item.Sum(x => x.TotalPrice),
                    UpOrDown = valueOfSameWithPurchase.Sum(x => x.TotalPrice) - item.Sum(x => x.TotalPrice) > 0 ? 0 : 1,
                };
                listValuePurchaseOrderStatus.Add(dataReturn);
            }

            return listValuePurchaseOrderStatus;
        }

        public async Task CreateAndUpdatePurchaseOrderWhenPaymentFromStripe(Session stripeSession, Event eventStripe)
        {
            var dataPurchaseModel = await DistributedCacheRedis.GetDataToDistributedCache<PurcharseCourseModel>(_distributedCache, stripeSession.ClientReferenceId);
            if (dataPurchaseModel != null)
            {
                switch (eventStripe.Type)
                {
                    case Events.CheckoutSessionCompleted:
                        if (stripeSession.PaymentStatus == "paid")
                        {
                            var getDataPurchasePaid = await _pucharseCourseRepository.GetObjectAsync(x => x.PurcharseCode == dataPurchaseModel.PurcharseCode);
                            if (getDataPurchasePaid != null)
                            {
                                await UpdatePurchaseStatus(getDataPurchasePaid, ETypeOfStatusOrder.Payment, stripeSession);
                            }
                            else
                            {
                                await CreateAndUpdatePurchase(dataPurchaseModel, ETypeOfStatusOrder.Payment, stripeSession);
                            }
                        }
                        else
                        {
                            await CreateAndUpdatePurchase(dataPurchaseModel, ETypeOfStatusOrder.Request, stripeSession);
                        }
                        break;

                    case Events.CheckoutSessionAsyncPaymentSucceeded:

                        await CreateAndUpdatePurchase(dataPurchaseModel, ETypeOfStatusOrder.Payment, stripeSession);
                        break;

                    case Events.CheckoutSessionAsyncPaymentFailed:
                        var getDataPurchaseFaild = await _pucharseCourseRepository.GetObjectAsync(x => x.PurcharseCode == dataPurchaseModel.PurcharseCode);
                        await UpdatePurchaseStatus(getDataPurchaseFaild, ETypeOfStatusOrder.Pending, stripeSession);
                        break;

                    default:
                        break;
                }
            }
        }

        private async Task UpdatePurchaseStatus(PurcharseCourseEntities purchaseModel, ETypeOfStatusOrder status, Session stripeSession)
        {
            purchaseModel.PurcharseStatus = (int)status;
            purchaseModel.PurchaseDate = LocalDateTime.FromDateTime(DateTime.UtcNow);
            purchaseModel.CheckoutSessionId = stripeSession.Id;
            purchaseModel.PaymentIntent = stripeSession.PaymentIntentId;
            await _pucharseCourseRepository.UpdateAsync(purchaseModel);
        }

        private async Task CreateAndUpdatePurchase(PurcharseCourseModel purchaseModel, ETypeOfStatusOrder status, Session stripeSession)
        {
            purchaseModel.PurcharseStatus = (int)status;
            purchaseModel.PurchaseDate = LocalDateTime.FromDateTime(DateTime.UtcNow);
            purchaseModel.CheckoutSessionId = stripeSession.Id;
            purchaseModel.PaymentIntent = stripeSession.PaymentIntentId;
            await CreateRequestPurchase(purchaseModel);
        }
    }
}