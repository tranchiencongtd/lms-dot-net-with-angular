using AutoMapper;
using HDNXUdemyData.Entities;
using HDNXUdemyData.IRepository;
using HDNXUdemyModel.Constant;
using HDNXUdemyModel.Model;
using HDNXUdemyModel.SystemExceptions;
using HDNXUdemyServices.CommonFunction;
using HDNXUdemyServices.IServices;

namespace HDNXUdemyServices.Services
{
    public class StudentServices : IStudentServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IStudentPromotionRepository _studentPromotionRepository;
        private readonly IStudentProcessRepository _studentProcessRepository;
        private readonly IBookmarkCourseRepository _bookmarkCourseRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IRPPurcharseCourseDetailsRepository _purcharseCourseDetailsRepository;
        private readonly IRCourseEvaluationRepository _courseEvaluationRepository;

        public StudentServices(IUserRepository userRepository, IMapper mapper, IStudentPromotionRepository studentPromotionRepository,
            IStudentProcessRepository studentProcessRepository, IBookmarkCourseRepository bookmarkCourseRepository, ICategoryRepository categoryRepository,
            ICourseRepository courseRepository, IRPPurcharseCourseDetailsRepository purcharseCourseDetailsRepository, IRCourseEvaluationRepository courseEvaluationRepository)
        {
            _userRepository = userRepository ?? throw new ProjectException(nameof(_userRepository));
            _mapper = mapper ?? throw new ProjectException(nameof(_mapper));
            _studentPromotionRepository = studentPromotionRepository ?? throw new ProjectException(nameof(_studentPromotionRepository));
            _studentProcessRepository = studentProcessRepository ?? throw new ProjectException(nameof(_studentProcessRepository));
            _bookmarkCourseRepository = bookmarkCourseRepository ?? throw new ProjectException(nameof(_bookmarkCourseRepository));
            _categoryRepository = categoryRepository ?? throw new ProjectException(nameof(_categoryRepository));
            _courseRepository = courseRepository ?? throw new ProjectException(nameof(_courseRepository));
            _purcharseCourseDetailsRepository = purcharseCourseDetailsRepository ?? throw new ProjectException(nameof(_purcharseCourseDetailsRepository));
            _courseEvaluationRepository = courseEvaluationRepository ?? throw new ProjectException(nameof(_courseEvaluationRepository)); ;
        }

        public async Task<bool> CreateStudent(UserModel model)
        {
            bool isCheckEmailExit = string.IsNullOrEmpty((await _userRepository.GetObjectAsync(x => x.Email == model.Email))?.Email);
            if (isCheckEmailExit)
            {
                model.IsRequestTeacher = false;
                model.RoleId = (int)ERoles.Student;
                model.Password = Generator.GeneratePassword(10);
                var dataInsert = _mapper.Map<UserEntities>(model);
                return await _userRepository.AddAsync(dataInsert);
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UpdateStatusStudent(long id, UserModel model)
        {
            var getData = await _userRepository.GetByIdAsync(id) ?? new UserEntities();
            getData.Status = model.Status;
            return await _userRepository.UpdateStatusAsync(getData);
        }

        public async Task<bool> UpdateBecomeToTeacher(long id)
        {
            var getData = await _userRepository.GetByIdAsync(id) ?? new UserEntities();
            getData.IsRequestTeacher = true;
            getData.RoleId = (int)ERoles.Teacher;
            return await _userRepository.UpdateAsync(getData);
        }

        public async Task<bool> UpdateStudent(long id, UserModel model)
        {
            var getData = await _userRepository.GetByIdAsync(id) ?? new UserEntities();
            getData.Status = model.Status;
            getData.Email = model.Email;
            getData.PictureUrl = model.PictureUrl;
            getData.Phone = model.Phone;
            getData.General = model.General;
            getData.PublicId = model.PublicId;
            getData.RoleId = model.RoleId;
            getData.Name = model.Name;
            getData.Password = model.Password;
            return await _userRepository.UpdateAsync(getData);
        }

        public async Task<List<UserModel>> GetListStudents()
        {
            var getData = await _userRepository.GetAsync(x => x.RoleId == (int)ERoles.Student);
            return _mapper.Map<List<UserModel>>(getData);
        }

        public async Task<List<UserModel>> GetListUserManager()
        {
            var getData = await _userRepository.GetAsync(x => x.RoleId == (int)ERoles.User || x.RoleId == (int)ERoles.Admin);
            return _mapper.Map<List<UserModel>>(getData);
        }

        public async Task<List<UserModel>> GetListTeachers()
        {
            var getData = await _userRepository.GetAsync(x => x.RoleId == (int)ERoles.Teacher);
            return _mapper.Map<List<UserModel>>(getData);
        }

        public async Task<UserModel> GetStudent(long id)
        {
            var getData = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<UserModel>(getData);
        }

        public async Task<bool> CreateStudentPromotions(StudentPromotionModel model)
        {
            var dataInsert = _mapper.Map<StudentPromotionEntities>(model);
            return await _studentPromotionRepository.AddAsync(dataInsert);
        }

        public async Task<bool> UpdateStatusStudentPromotions(long id, StudentPromotionModel model)
        {
            var getData = await _studentPromotionRepository.GetByIdAsync(id) ?? new StudentPromotionEntities();
            getData.Status = model.Status;
            return await _studentPromotionRepository.UpdateStatusAsync(getData);
        }

        public async Task<bool> UpdateInformationStudentPromotions(long id, StudentPromotionModel model)
        {
            var getData = await _studentPromotionRepository.GetByIdAsync(id) ?? new StudentPromotionEntities();
            getData.Status = model.Status;
            getData.IdCourse = model.IdCourse;
            getData.IdStudent = model.IdStudent;
            getData.IdPromotion = model.IdPromotion;
            getData.CodePromotion = model.CodePromotion;
            getData.ValuePromotion = model.ValuePromotion;
            return await _studentPromotionRepository.UpdateAsync(getData);
        }

        public async Task<List<StudentPromotionModel>> GetListStudentPromotions()
        {
            var getData = await _studentPromotionRepository.GetAllAsync();
            return _mapper.Map<List<StudentPromotionModel>>(getData);
        }

        public async Task<StudentPromotionModel> GetStudentPromotions(long id)
        {
            var getData = await _studentPromotionRepository.GetByIdAsync(id);
            return _mapper.Map<StudentPromotionModel>(getData);
        }

        public async Task<bool> CreateStudentProcess(StudentProcessModel model)
        {
            var dataInsert = _mapper.Map<StudentProcessEntities>(model);
            return await _studentProcessRepository.AddAsync(dataInsert);
        }

        public async Task<bool> UpdateStatusStudentProcess(long id, StudentProcessModel model)
        {
            var getData = await _studentProcessRepository.GetByIdAsync(id) ?? new StudentProcessEntities();
            getData.Status = model.Status;
            return await _studentProcessRepository.UpdateStatusAsync(getData);
        }

        public async Task<bool> UpdateInformationStudentProcess(long id, StudentProcessModel model)
        {
            var getData = await _studentProcessRepository.GetByIdAsync(id) ?? new StudentProcessEntities();
            getData.Status = model.Status;
            getData.IdCourse = model.IdCourse;
            getData.NumberContentOfCourse = model.NumberContentOfCourse;
            getData.LastContentOfContent = model.LastContentOfContent;
            return await _studentProcessRepository.UpdateAsync(getData);
        }

        public async Task<List<StudentProcessModel>> GetListStudentProcess()
        {
            var getData = await _studentProcessRepository.GetAllAsync();
            return _mapper.Map<List<StudentProcessModel>>(getData);
        }

        public async Task<StudentProcessModel> GetStudentProcess(long id)
        {
            var getData = await _studentProcessRepository.GetByIdAsync(id);
            return _mapper.Map<StudentProcessModel>(getData);
        }

        public async Task<bool> CreateStudentBookmarkCourse(BookmarkCourseModel model)
        {
            var dataInsert = _mapper.Map<BookmarkCourseEntities>(model);
            var isCheckData = await _bookmarkCourseRepository.GetObjectAsync(x => x.IdCourse == model.IdCourse && x.IdStudent == model.IdStudent);
            if (isCheckData == null)
            {
                return await _bookmarkCourseRepository.AddAsync(dataInsert);
            }
            else
            {
                return await _bookmarkCourseRepository.DeleteByKey(isCheckData.Id);
            }
        }

        public async Task<bool> UpdateStatusStudentBookmarkCourse(long id, BookmarkCourseModel model)
        {
            var getData = await _bookmarkCourseRepository.GetByIdAsync(id) ?? new BookmarkCourseEntities();
            getData.Status = model.Status;
            return await _bookmarkCourseRepository.UpdateStatusAsync(getData);
        }

        public async Task<List<CourseModel>> GetListStudentBookmarkCourse(long idUser)
        {
            var getData = await _bookmarkCourseRepository.GetAsync(x => x.IdStudent == idUser);
            var listIdOfCoureBookmark = getData.DistinctBy(x => x.IdCourse).Select(x => x.IdCourse).ToList();

            var getDataOfCourse = await _courseRepository.GetAsync(x => listIdOfCoureBookmark.Contains(x.Id));
            var getCategory = await _categoryRepository.GetAllAsync();
            var resultMapping = _mapper.Map<List<CourseModel>>(getDataOfCourse);

            foreach (var item in resultMapping)
            {
                var isPurchaseCourseDetails = await _purcharseCourseDetailsRepository.GetAsync(x => x.IdCourse == item.Id && x.IdStudent == idUser);
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
                item.UserName = (await _userRepository.GetByIdAsync(idUser))?.Name;
                item.ProcessCourseName = ((ProcessVideo)item.ProcessCourse).GetEnumDescription();
                item.IsPurchase = isPurchaseCourseDetails.Any();
                item.IsBookMark = true;
            }
            return resultMapping;
        }

        public async Task<bool> DeleteStudentBookmarkCourse(long id)
        {
            return await _bookmarkCourseRepository.DeleteByKey(id);
        }

        public async Task<List<string?>> GetListUserNameRegisterForCourse(long idCourse)
        {
            var getListDataStudentPurchase = await _purcharseCourseDetailsRepository.GetAsync(x => x.IdCourse == idCourse);
            List<long> listIdStudent = getListDataStudentPurchase.Select(x => x.IdStudent).ToList();
            return (await _userRepository.GetAsync(x => listIdStudent.Contains(x.Id))).Select(x => x.Name).ToList();
        }
    }
}