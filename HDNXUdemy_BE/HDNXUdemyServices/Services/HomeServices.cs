using AutoMapper;
using HDNXUdemyData.IRepository;
using HDNXUdemyModel.Constant;
using HDNXUdemyModel.Model;
using HDNXUdemyModel.ResponModel;
using HDNXUdemyModel.SystemExceptions;
using HDNXUdemyServices.IServices;

namespace HDNXUdemyServices.Services
{
    public class HomeServices : IHomeServices
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IRPPartnerRepository _partnerRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBookmarkCourseRepository _bookmarkCourseRepository;
        private readonly IMapper _mapper;

        public HomeServices(ICourseRepository courseRepository, IRPPartnerRepository partnerRepository,
            ICategoryRepository categoryRepository, IMapper mapper, IBookmarkCourseRepository bookmarkCourseRepository)
        {
            _courseRepository = courseRepository ?? throw new ProjectException(nameof(_courseRepository));
            _partnerRepository = partnerRepository ?? throw new ProjectException(nameof(_partnerRepository));
            _categoryRepository = categoryRepository ?? throw new ProjectException(nameof(_categoryRepository));
            _bookmarkCourseRepository = bookmarkCourseRepository ?? throw new ProjectException(nameof(_bookmarkCourseRepository));
            _mapper = mapper ?? throw new ProjectException(nameof(_categoryRepository));
        }

        public async Task<HomeModel> GetDataForHome(long? idUser)
        {
            var returnValue = new HomeModel()
            {
                Partners = new List<PartnerModel>(),
                ListContentData = new List<ListContentOfCourse>()
            };
            var getDataOfCategory = await _categoryRepository.GetAllAsync();
            var getDataOfPartner = _mapper.Map<List<PartnerModel>>(await _partnerRepository.GetAllAsync());
            returnValue.Partners.AddRange(getDataOfPartner);
            var getDataOfBestIsBuy = new ListContentOfCourse()
            {
                NameContent = "The best-selling course",
                ListDataOfContent = await GetBookMarkForCourse(_mapper.Map<List<CourseModel>>((await _courseRepository
                .GetAsync(x => x.ProcessCourse == (int)ProcessVideo.Public)).OrderBy(x => x.TotalStudentRegister).Take(10)), idUser),
            };
            returnValue.ListContentData.Add(getDataOfBestIsBuy);
            var getDataOfBestIsDisCount = new ListContentOfCourse()
            {
                NameContent = "Promotional course",
                ListDataOfContent = await GetBookMarkForCourse(_mapper.Map<List<CourseModel>>((await _courseRepository
                .GetAsync(x => x.IsDiscount == true && x.ProcessCourse == (int)ProcessVideo.Public)).OrderBy(x => x.CreateDate).Take(10)), idUser),
            };
            returnValue.ListContentData.Add(getDataOfBestIsDisCount);
            foreach (var item in getDataOfCategory)
            {
                var getDataItem = new ListContentOfCourse()
                {
                    NameContent = item.Name,
                    ListDataOfContent = await GetBookMarkForCourse(_mapper.Map<List<CourseModel>>((await _courseRepository
                    .GetAsync(x => x.IdCategory == item.Id && x.ProcessCourse == (int)ProcessVideo.Public)).Take(10)), idUser),
                };
                returnValue.ListContentData.Add(getDataItem);
            }

            return returnValue;
        }

        public async Task<List<CourseModel>> GetBookMarkForCourse(List<CourseModel> listCourse, long? idUser)
        {
            var getDataBookMarkOfStudent = await _bookmarkCourseRepository.GetAsync(x => x.IdStudent == idUser);
            foreach (var item in listCourse)
            {
                item.IsBookMark = getDataBookMarkOfStudent?.FirstOrDefault(x => x.IdCourse == item.Id) != null ? true : false;
            }
            return listCourse;
        }
    }
}