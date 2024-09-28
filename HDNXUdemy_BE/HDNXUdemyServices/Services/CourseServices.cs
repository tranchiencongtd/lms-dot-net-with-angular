using AutoMapper;
using DNXUdemyData.Entities;
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

namespace HDNXUdemyServices.Services
{
    public class CourseServices : ICourseServices
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IContentCourseRepository _contentCourseRepository;
        private readonly IContentCourseDetailRepository _contentCourseDetailRepository;
        private readonly IRTheadQuestionCourseRepository _theadQuestionCourseRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPurcharseCourseRepository _pucharseCourseRepository;
        private readonly IRPPurcharseCourseDetailsRepository _purcharseCourseDetailsRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRCourseEvaluationRepository _courseEvaluationRepository;
        private readonly IRDetailTheadQuestionCourseRepository _detailsTheadQuestionCourseRepository;

        public CourseServices(ICourseRepository courseRepository, IContentCourseRepository contentCourseRepository, IContentCourseDetailRepository contentCourseDetailRepository,
            IRTheadQuestionCourseRepository theadQuestionCourseRepository, IMapper mapper, ICategoryRepository categoryRepository,
            IUserRepository userRepository, IPurcharseCourseRepository pucharseCourseRepository, IRPPurcharseCourseDetailsRepository purcharseCourseDetailsRepository,
            IHttpContextAccessor httpContextAccessor, IRCourseEvaluationRepository courseEvaluationRepository, IRDetailTheadQuestionCourseRepository detailsTheadQuestionCourseRepository)
        {
            _courseRepository = courseRepository ?? throw new ProjectException(nameof(_courseRepository));
            _contentCourseRepository = contentCourseRepository ?? throw new ProjectException(nameof(_contentCourseRepository));
            _contentCourseDetailRepository = contentCourseDetailRepository ?? throw new ProjectException(nameof(_contentCourseDetailRepository));
            _theadQuestionCourseRepository = theadQuestionCourseRepository ?? throw new ProjectException(nameof(_theadQuestionCourseRepository));
            _categoryRepository = categoryRepository ?? throw new ProjectException(nameof(_categoryRepository));
            _userRepository = userRepository ?? throw new ProjectException(nameof(_userRepository));
            _mapper = mapper ?? throw new ProjectException(nameof(_mapper));
            _pucharseCourseRepository = pucharseCourseRepository ?? throw new ProjectException(nameof(_pucharseCourseRepository));
            _purcharseCourseDetailsRepository = purcharseCourseDetailsRepository ?? throw new ProjectException(nameof(_purcharseCourseDetailsRepository));
            _httpContextAccessor = httpContextAccessor ?? throw new ProjectException(nameof(_httpContextAccessor));
            _courseEvaluationRepository = courseEvaluationRepository ?? throw new ProjectException(nameof(_courseEvaluationRepository));
            _detailsTheadQuestionCourseRepository = detailsTheadQuestionCourseRepository ?? throw new ProjectException(nameof(_detailsTheadQuestionCourseRepository)); ;
        }

        public async Task<CourseModel> CreateCourse(CourseModel model)
        {
            var dataInsert = _mapper.Map<CourseEntities>(model);
            var dataAfterInsert = _mapper.Map<CourseModel>(await _courseRepository.AddReturnModelAsync(dataInsert));
            return dataAfterInsert;
        }

        public async Task<bool> UpdateStatusCourse(long id, int status, int processCourse)
        {
            var getData = await _courseRepository.GetByIdAsync(id) ?? new CourseEntities();
            getData.Status = status;
            getData.ProcessCourse = processCourse;
            return await _courseRepository.UpdateAsync(getData);
        }

        public async Task<bool> UpdateInformationCourse(long id, CourseModel model)
        {
            var getData = await _courseRepository.GetByIdAsync(id) ?? new CourseEntities();
            var dataInsert = _mapper.Map<CourseEntities>(model);
            dataInsert.CreateDate = getData.CreateDate;
            return await _courseRepository.UpdateAsync(dataInsert);
        }

        public async Task<List<CourseModel>> GetListCourseForAdmin()
        {
            var getData = await _courseRepository.GetAllAsync();
            var getCategory = await _categoryRepository.GetAllAsync();
            var resultMapping = _mapper.Map<List<CourseModel>>(getData);
            foreach (var item in resultMapping)
            {
                var getDataOfVoteStar = await _courseEvaluationRepository.GetAsync(x => x.IdCourse == item.Id);
                var getVoteData = HelperFunction.CalculatorToTalStartOfCourse(getDataOfVoteStar.ToList());
                item.TotalVoteOfCourse = getDataOfVoteStar.Count();
                item.Vote1Star = getVoteData.Item1;
                item.Vote2Star = getVoteData.Item2;
                item.Vote3Star = getVoteData.Item3;
                item.Vote4Star = getVoteData.Item4;
                item.Vote5Star = getVoteData.Item5;
                item.AverageScore = getVoteData.Item6;
                item.CategoryName = getCategory.Where(x => x.Id == item.IdCategory).FirstOrDefault()?.Name;
                item.UserName = "Admin";
                item.ProcessCourseName = ((ProcessVideo)item.ProcessCourse).GetEnumDescription();
            }
            return resultMapping;
        }

        public async Task<List<CourseModel>> GetListCourseOfStudent(long idStudent)
        {
            var getIdOfPurchaseOrder = await _pucharseCourseRepository.
                GetAsync(x => x.IdStudent == idStudent && x.PurcharseStatus == (int)ETypeOfStatusOrder.Payment && x.Status == (int)EStatus.Active);
            var getIdOfCourseOfStudent = await _purcharseCourseDetailsRepository.GetAsync(item => getIdOfPurchaseOrder.Select(x => x.Id).Contains(item.IdPurchaseOrder));
            var getData = await _courseRepository.GetAsync(item => getIdOfCourseOfStudent.Select(x => x.IdCourse).Contains(item.Id));
            var getCategory = await _categoryRepository.GetAllAsync();
            var resultMapping = _mapper.Map<List<CourseModel>>(getData);
            foreach (var item in resultMapping)
            {
                var getDataOfVoteStar = await _courseEvaluationRepository.GetAsync(x => x.IdCourse == item.Id);
                var getVoteData = HelperFunction.CalculatorToTalStartOfCourse(getDataOfVoteStar.ToList());
                item.TotalVoteOfCourse = getDataOfVoteStar.Count();
                item.Vote1Star = getVoteData.Item1;
                item.Vote2Star = getVoteData.Item2;
                item.Vote3Star = getVoteData.Item3;
                item.Vote4Star = getVoteData.Item4;
                item.Vote5Star = getVoteData.Item5;
                item.AverageScore = getVoteData.Item6;
                item.CategoryName = getCategory.Where(x => x.Id == item.IdCategory).FirstOrDefault()?.Name;
                item.UserName = (await _userRepository.GetByIdAsync(item.CreateBy))?.Name;
            }
            return resultMapping;
        }

        public async Task<GetCourseWithDetailsContent> GetDetailCourse(long id, bool isAdmin)
        {
            var getData = await _courseRepository.GetByIdAsync(id);
            var resultMapping = _mapper.Map<GetCourseWithDetailsContent>(getData);
            long idCurrentUser = _httpContextAccessor.HttpContext == null ? 0 : long.Parse(_httpContextAccessor.HttpContext.User.Claims
                                .Where(x => x.Type == "user-id").FirstOrDefault()?.Value ?? "0");
            bool isPurchase = false;
            if (isAdmin)
            {
                isAdmin = true;
            }
            else
            {
                var isPurchaseCourseDetails = await _purcharseCourseDetailsRepository.GetAsync(x => x.IdCourse == id && x.IdStudent == idCurrentUser);

                if (isPurchaseCourseDetails.Any())
                {
                    var isPurchaseCourse = await _pucharseCourseRepository.GetByIdAsync(isPurchaseCourseDetails.FirstOrDefault().IdPurchaseOrder);
                    isPurchase = isAdmin = isPurchaseCourseDetails.Any() && isPurchaseCourse?.PurcharseStatus == (int)ETypeOfStatusOrder.Payment;
                }
            }

            if (getData != null && resultMapping != null)
            {
                var resultCourse = _mapper.Map<List<CourseModel>>((await _courseRepository.GetAsync(x => x.IdCategory == getData.IdCategory)).Take(3));
                var resultAuthor = _mapper.Map<UserModel>(await _userRepository.GetObjectAsync(x => x.Id == getData.CreateBy));
                var getCategory = await _categoryRepository.GetAllAsync();
                var getDataOfVoteStar = await _courseEvaluationRepository.GetAsync(x => x.IdCourse == resultMapping.Id);
                var getVoteData = HelperFunction.CalculatorToTalStartOfCourse(getDataOfVoteStar.ToList());
                resultMapping.TotalVoteOfCourse = getDataOfVoteStar.Count();
                resultMapping.Vote1Star = getVoteData.Item1;
                resultMapping.Vote2Star = getVoteData.Item2;
                resultMapping.Vote3Star = getVoteData.Item3;
                resultMapping.Vote4Star = getVoteData.Item4;
                resultMapping.Vote5Star = getVoteData.Item5;
                resultMapping.AverageScore = getVoteData.Item6;
                resultMapping.CategoryName = getCategory.Where(x => x.Id == resultMapping.IdCategory).FirstOrDefault()?.Name;
                resultMapping.UserName = (await _userRepository.GetByIdAsync(resultMapping.CreateBy))?.Name;
                resultMapping.ProcessCourseName = ((ProcessVideo)resultMapping?.ProcessCourse).GetEnumDescription();
                resultMapping.ListContentCourseDetails = await GetContentOfCourse(getData.Id, isAdmin);
                resultMapping.ListCourseRate = resultCourse;
                resultMapping.Author = resultAuthor;
                resultMapping.FileUploadUrlStream = $"{ProjectConfig.APIUrlGetVideoMp4}{resultMapping.FileUrl}";
                resultMapping.IsPurchase = isPurchase;
            }

            return resultMapping ?? new GetCourseWithDetailsContent();
        }

        public async Task<List<StudentProcessModel>> GetListStudentProcess()
        {
            var getData = await _courseRepository.GetAllAsync();
            return _mapper.Map<List<StudentProcessModel>>(getData);
        }

        public async Task<StudentProcessModel> GetStudentProcess(long id)
        {
            var getData = await _courseRepository.GetByIdAsync(id);
            return _mapper.Map<StudentProcessModel>(getData);
        }

        public async Task<bool> AddCommentOfStudentForCourse(CourseEvaluationModel model)
        {
            var insertData = _mapper.Map<CourseEvaluationEntities>(model);
            return await _courseEvaluationRepository.AddAsync(insertData);
        }

        public async Task<List<CourseEvaluationModel>> GetListCoursEvaluation(long idCourse)
        {
            var getData = await _courseEvaluationRepository.GetAsync(x => x.IdCourse == idCourse && x.Status == (int)EStatus.Active);
            var returnData = _mapper.Map<List<CourseEvaluationModel>>(getData);
            foreach (var item in returnData)
            {
                item.Users = _mapper.Map<UserModel>(await _userRepository.GetByIdAsync(item.CreateBy));
            }
            return returnData;
        }

        public async Task<bool> UpdateStatusCommentCourse(long id, CourseEvaluationModel model)
        {
            var getData = await _courseEvaluationRepository.GetByIdAsync(id) ?? new CourseEvaluationEntities();
            getData.Status = model.Status;
            return await _courseEvaluationRepository.UpdateStatusAsync(getData);
        }

        public async Task<bool> UpdateInformationCommentCourse(long id, CourseEvaluationModel model)
        {
            var getData = await _courseEvaluationRepository.GetByIdAsync(id) ?? new CourseEvaluationEntities();
            var dataInsert = _mapper.Map<CourseEvaluationEntities>(model);
            dataInsert.CreateDate = getData.CreateDate;
            return await _courseEvaluationRepository.UpdateAsync(dataInsert);
        }

        public async Task<CourseEvaluationModel> GetCommentCourse(long id)
        {
            var getData = await _courseEvaluationRepository.GetByIdAsync(id);
            return _mapper.Map<CourseEvaluationModel>(getData);
        }

        public async Task<bool> CreateContentCourse(ContentCourseModel model)
        {
            var dataInsert = _mapper.Map<ContentCourseEntities>(model);
            return await _contentCourseRepository.AddAsync(dataInsert);
        }

        public async Task<bool> UpdateStatusContentCourse(long id, ContentCourseModel model)
        {
            var getData = await _contentCourseRepository.GetByIdAsync(id) ?? new ContentCourseEntities();
            getData.Status = model.Status;
            return await _contentCourseRepository.UpdateStatusAsync(getData);
        }

        public async Task<bool> UpdateInformationContentCourse(long id, ContentCourseModel model)
        {
            var getData = await _contentCourseRepository.GetByIdAsync(id) ?? new ContentCourseEntities();
            var dataInsert = _mapper.Map<ContentCourseEntities>(model);
            dataInsert.CreateDate = getData.CreateDate;
            return await _contentCourseRepository.UpdateAsync(dataInsert);
        }

        public async Task<List<ListContentWithDetailCourse>> GetListContentCourse(long idCourse)
        {
            var getData = await _contentCourseRepository.GetAsync(x => x.IdCourse == idCourse);
            var mappingDataContentCourse = _mapper.Map<List<ListContentWithDetailCourse>>(getData.OrderBy(x => x.CreateDate));
            foreach (var item in mappingDataContentCourse)
            {
                var getDataCourseDetail = await _contentCourseDetailRepository.GetAsync(x => x.IdContent == item.Id);
                item.ListContentCourseDetails = _mapper.Map<List<ContentCourseDetailModel>>(getDataCourseDetail);
            }

            return mappingDataContentCourse;
        }

        public async Task<ContentCourseModel> GetContentCourse(long id)
        {
            var getData = await _contentCourseRepository.GetByIdAsync(id);
            return _mapper.Map<ContentCourseModel>(getData);
        }

        public async Task<bool> CreateContentCourseDetails(ContentCourseDetailModel model)
        {
            var dataInsert = _mapper.Map<ContentCourseDetailEntities>(model);
            return await _contentCourseDetailRepository.AddAsync(dataInsert);
        }

        public async Task<bool> UpdateStatusContentCourseDetails(long id, ContentCourseDetailModel model)
        {
            var getData = await _contentCourseDetailRepository.GetByIdAsync(id) ?? new ContentCourseDetailEntities();
            getData.Status = model.Status;
            return await _contentCourseDetailRepository.UpdateStatusAsync(getData);
        }

        public async Task<bool> UpdateInformationContentCourseDetails(long id, ContentCourseDetailModel model)
        {
            var getData = await _contentCourseDetailRepository.GetByIdAsync(id) ?? new ContentCourseDetailEntities();
            var dataInsert = _mapper.Map<ContentCourseDetailEntities>(model);
            dataInsert.CreateDate = getData.CreateDate;
            return await _contentCourseDetailRepository.UpdateAsync(dataInsert);
        }

        public async Task<List<ContentCourseDetailModel>> GetListContentCourseDetails(long idContent)
        {
            var getData = await _contentCourseDetailRepository.GetAsync(x => x.IdContent == idContent);
            return _mapper.Map<List<ContentCourseDetailModel>>(getData);
        }

        public async Task<ContentCourseDetailModel> GetContentCourseDetails(long id, HttpRequest request)
        {
            var getData = await _contentCourseDetailRepository.GetByIdAsync(id);
            var resultMapping = _mapper.Map<ContentCourseDetailModel>(getData);
            resultMapping.FileUploadUrlStream = $"{ProjectConfig.APIUrlGetVideoStream}{resultMapping.FileNameVideo}";
            return resultMapping;
        }

        public async Task<List<ContentAndContentDetail>> GetContentOfCourse(long idCourse, bool isAdmin)
        {
            var resultData = new List<ContentAndContentDetail>();
            var getDataCourseContent = await _contentCourseRepository.GetAsync(x => x.IdCourse == idCourse);
            resultData = _mapper.Map<List<ContentAndContentDetail>>(getDataCourseContent.OrderBy(x => x.CreateDate));

            foreach (var item in resultData)
            {
                item.ContentAndContentDetails = new List<ContentCourseDetailModel>();
                if (isAdmin)
                {
                    item.ContentAndContentDetails = _mapper.Map<List<ContentCourseDetailModel>>(await _contentCourseDetailRepository.GetAsync(x => x.IdContent == item.Id));
                }
                else
                {
                    item.ContentAndContentDetails = _mapper.Map<List<ContentCourseDetailModel>>(await _contentCourseDetailRepository.GetAsync(x => x.IdContent == item.Id));
                    item.ContentAndContentDetails.ForEach(item =>
                    {
                        if (!item.IsLearningFree)
                        {
                            item.FileUploadUrlStream = null;
                        }
                    });
                }
            }

            return resultData;
        }

        public async Task<bool> CreateTheadQuestionCourse(TheadQuestionCourseModel model)
        {
            var dataInsert = _mapper.Map<TheadQuestionCourseEntities>(model);
            return await _theadQuestionCourseRepository.AddAsync(dataInsert);
        }

        public async Task<bool> UpdateStatusTheadQuestionCourse(long id, TheadQuestionCourseModel model)
        {
            var getData = await _theadQuestionCourseRepository.GetByIdAsync(id) ?? new TheadQuestionCourseEntities();
            getData.Status = model.Status;
            return await _theadQuestionCourseRepository.UpdateStatusAsync(getData);
        }

        public async Task<bool> UpdateInformationTheadQuestionCourse(long id, TheadQuestionCourseModel model)
        {
            var getData = await _theadQuestionCourseRepository.GetByIdAsync(id) ?? new TheadQuestionCourseEntities();
            var dataInsert = _mapper.Map<TheadQuestionCourseEntities>(model);
            dataInsert.CreateDate = getData.CreateDate;
            return await _theadQuestionCourseRepository.UpdateAsync(dataInsert);
        }

        public async Task<List<TheadQuestionCourseModel>> GetListTheadQuestionCourse(long idCourse)
        {
            var getData = await _theadQuestionCourseRepository.GetAsync(x => x.IdCourse == idCourse);
            return _mapper.Map<List<TheadQuestionCourseModel>>(getData);
        }

        public async Task<TheadQuestionCourseModel> GetTheadQuestionCourse(long id)
        {
            var getData = await _theadQuestionCourseRepository.GetByIdAsync(id);
            return _mapper.Map<TheadQuestionCourseModel>(getData);
        }

        public async Task<bool> CreateDetailsTheadQuestionCourse(DetailTheadQuestionCourseModel model)
        {
            var dataInsert = _mapper.Map<DetailTheadQuestionCourseEntities>(model);
            return await _detailsTheadQuestionCourseRepository.AddAsync(dataInsert);
        }

        public async Task<bool> UpdateStatusDetailsTheadQuestionCourse(long id, DetailTheadQuestionCourseModel model)
        {
            var getData = await _detailsTheadQuestionCourseRepository.GetByIdAsync(id) ?? new DetailTheadQuestionCourseEntities();
            getData.Status = model.Status;
            return await _detailsTheadQuestionCourseRepository.UpdateStatusAsync(getData);
        }

        public async Task<bool> UpdateInformationDetailsTheadQuestionCourse(long id, DetailTheadQuestionCourseModel model)
        {
            var getData = await _detailsTheadQuestionCourseRepository.GetByIdAsync(id) ?? new DetailTheadQuestionCourseEntities();
            var dataInsert = _mapper.Map<DetailTheadQuestionCourseEntities>(model);
            dataInsert.CreateDate = getData.CreateDate;
            return await _detailsTheadQuestionCourseRepository.UpdateAsync(dataInsert);
        }

        public async Task<List<DetailTheadQuestionCourseModel>> GetListDetailsTheadQuestionCourse(long idThear)
        {
            var getData = await _detailsTheadQuestionCourseRepository.GetAsync(x => x.IdTheadQuestionCourse == idThear);
            return _mapper.Map<List<DetailTheadQuestionCourseModel>>(getData);
        }

        public async Task<List<CourseModel>> GetListCourseAsCategory(long idCategory)
        {
            var getData = await _courseRepository.GetAsync(x => x.IdCategory == idCategory);
            return _mapper.Map<List<CourseModel>>(getData);
        }

        public async Task<bool> LikeForCommentCourse(long id)
        {
            var getData = await _courseEvaluationRepository.GetByIdAsync(id);
            getData.Like += 1;
            return await _courseEvaluationRepository.UpdateAsync(getData);
        }

        public async Task<bool> DisLikeForCommentCourse(long id)
        {
            var getData = await _courseEvaluationRepository.GetByIdAsync(id);
            getData.DisLike += 1;
            return await _courseEvaluationRepository.UpdateAsync(getData);
        }
    }
}