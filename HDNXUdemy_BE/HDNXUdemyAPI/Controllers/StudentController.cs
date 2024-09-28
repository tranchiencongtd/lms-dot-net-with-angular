using Asp.Versioning;
using HDNXUdemyModel.Base;
using HDNXUdemyModel.Constant;
using HDNXUdemyModel.Model;
using HDNXUdemyModel.SystemExceptions;
using HDNXUdemyServices.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HDNXUdemyAPI.Controllers
{
    /// <summary>
    /// StudentController
    /// </summary>
    [ApiVersion(VersionApi.Version1)]
    [Route(RouterControllerName.Student)]
    public class StudentController : BaseController
    {
        private readonly IStudentServices _studentServices;
        private readonly ICourseServices _courseServices;

        /// <summary>
        /// StudentController
        /// </summary>
        /// <param name="studentServices"></param>
        /// <param name="courseServices"></param>
        /// <exception cref="ProjectException"></exception>
        public StudentController(IStudentServices studentServices, ICourseServices courseServices)
        {
            _studentServices = studentServices ?? throw new ProjectException(nameof(_studentServices));
            _courseServices = courseServices ?? throw new ProjectException(nameof(_courseServices));
        }

        /// <summary>
        /// CreateStudent
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<RepositoryModel<bool>> CreateStudent(UserModel model)
        {
            RepositoryModel<bool> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new bool(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _studentServices.CreateStudent(model);
            return result;
        }

        /// <summary>
        /// UpdateStatusStudent
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("status/{id}")]
        public async Task<RepositoryModel<bool>> UpdateStatusStudent(long id, UserModel model)
        {
            RepositoryModel<bool> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new bool(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _studentServices.UpdateStatusStudent(id, model);
            return result;
        }

        /// <summary>
        /// UpdateStudent
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<RepositoryModel<bool>> UpdateStudent(long id, UserModel model)
        {
            RepositoryModel<bool> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new bool(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _studentServices.UpdateStudent(id, model);
            return result;
        }

        /// <summary>
        /// GetListStudents
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<RepositoryModel<List<UserModel>>> GetListStudents()
        {
            RepositoryModel<List<UserModel>> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new List<UserModel>(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _studentServices.GetListStudents();
            return result;
        }

        /// <summary>
        /// GetStudent
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<RepositoryModel<UserModel>> GetStudent(long id)
        {
            RepositoryModel<UserModel> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new UserModel(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _studentServices.GetStudent(id);
            return result;
        }

        /// <summary>
        /// GetListUserManager
        /// </summary>
        /// <returns></returns>
        [HttpGet("account-user")]
        public async Task<RepositoryModel<List<UserModel>>> GetListUserManager()
        {
            RepositoryModel<List<UserModel>> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new List<UserModel>(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _studentServices.GetListUserManager();
            return result;
        }

        /// <summary>
        /// GetListTeachers
        /// </summary>
        /// <returns></returns>
        [HttpGet("account-teacher")]
        public async Task<RepositoryModel<List<UserModel>>> GetListTeachers()
        {
            RepositoryModel<List<UserModel>> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new List<UserModel>(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _studentServices.GetListTeachers();
            return result;
        }

        /// <summary>
        /// CreateStudentPromotions
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("student-promotion")]
        public async Task<RepositoryModel<bool>> CreateStudentPromotions(StudentPromotionModel model)
        {
            RepositoryModel<bool> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new bool(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _studentServices.CreateStudentPromotions(model);
            return result;
        }

        /// <summary>
        /// UpdateStatusStudentPromotions
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("student-promotion/status/{id}")]
        public async Task<RepositoryModel<bool>> UpdateStatusStudentPromotions(long id, StudentPromotionModel model)
        {
            RepositoryModel<bool> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new bool(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _studentServices.UpdateStatusStudentPromotions(id, model);
            return result;
        }

        /// <summary>
        /// UpdateInformationStudentPromotions
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("student-promotion/{id}")]
        public async Task<RepositoryModel<bool>> UpdateInformationStudentPromotions(long id, StudentPromotionModel model)
        {
            RepositoryModel<bool> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new bool(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _studentServices.UpdateInformationStudentPromotions(id, model);
            return result;
        }

        /// <summary>
        /// GetListStudentPromotions
        /// </summary>
        /// <returns></returns>
        [HttpGet("student-promotion")]
        public async Task<RepositoryModel<List<StudentPromotionModel>>> GetListStudentPromotions()
        {
            RepositoryModel<List<StudentPromotionModel>> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new List<StudentPromotionModel>(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _studentServices.GetListStudentPromotions();
            return result;
        }

        /// <summary>
        /// GetStudentPromotions
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("student-promotion/{id}")]
        public async Task<RepositoryModel<StudentPromotionModel>> GetStudentPromotions(long id)
        {
            RepositoryModel<StudentPromotionModel> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new StudentPromotionModel(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _studentServices.GetStudentPromotions(id);
            return result;
        }

        /// <summary>
        /// CreateStudentProcess
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("student-process")]
        public async Task<RepositoryModel<bool>> CreateStudentProcess(StudentProcessModel model)
        {
            RepositoryModel<bool> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new bool(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _studentServices.CreateStudentProcess(model);
            return result;
        }

        /// <summary>
        /// UpdateStatusStudentProcess
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("student-process/status/{id}")]
        public async Task<RepositoryModel<bool>> UpdateStatusStudentProcess(long id, StudentProcessModel model)
        {
            RepositoryModel<bool> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new bool(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _studentServices.UpdateStatusStudentProcess(id, model);
            return result;
        }

        /// <summary>
        /// UpdateInformationStudentProcess
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("student-process/{id}")]
        public async Task<RepositoryModel<bool>> UpdateInformationStudentProcess(long id, StudentProcessModel model)
        {
            RepositoryModel<bool> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new bool(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _studentServices.UpdateInformationStudentProcess(id, model);
            return result;
        }

        /// <summary>
        /// GetListStudentProcess
        /// </summary>
        /// <returns></returns>
        [HttpGet("student-process")]
        public async Task<RepositoryModel<List<StudentProcessModel>>> GetListStudentProcess()
        {
            RepositoryModel<List<StudentProcessModel>> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new List<StudentProcessModel>(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _studentServices.GetListStudentProcess();
            return result;
        }

        /// <summary>
        /// GetStudentProcess
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("student-process/{id}")]
        public async Task<RepositoryModel<StudentProcessModel>> GetStudentProcess(long id)
        {
            RepositoryModel<StudentProcessModel> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new StudentProcessModel(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _studentServices.GetStudentProcess(id);
            return result;
        }

        /// <summary>
        /// CreateBookmarkCourse
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("bookmark-course")]
        public async Task<RepositoryModel<bool>> CreateBookmarkCourse(BookmarkCourseModel model)
        {
            RepositoryModel<bool> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new bool(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _studentServices.CreateStudentBookmarkCourse(model);
            return result;
        }

        /// <summary>
        /// UpdateStatusBookmarkCourse
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("bookmark-course/status/{id}")]
        public async Task<RepositoryModel<bool>> UpdateStatusBookmarkCourse(long id, BookmarkCourseModel model)
        {
            RepositoryModel<bool> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new bool(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _studentServices.UpdateStatusStudentBookmarkCourse(id, model);
            return result;
        }

        /// <summary>
        /// GetListBookmarkCourse
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        [HttpGet("bookmark-course/{idUser}")]
        public async Task<RepositoryModel<List<CourseModel>>> GetListBookmarkCourse(long idUser)
        {
            RepositoryModel<List<CourseModel>> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new List<CourseModel>(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _studentServices.GetListStudentBookmarkCourse(idUser);
            return result;
        }

        /// <summary>
        /// RemoveBookmarkCourse
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("bookmark-course/{id}")]
        public async Task<RepositoryModel<bool>> RemoveBookmarkCourse(long id)
        {
            RepositoryModel<bool> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new bool(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _studentServices.DeleteStudentBookmarkCourse(id);
            return result;
        }

        /// <summary>
        /// GetCoursesOfStudent
        /// </summary>
        /// <param name="idStudent"></param>
        /// <returns></returns>
        [HttpGet("student-course/{idStudent}")]
        public async Task<RepositoryModel<List<CourseModel>>> GetCoursesOfStudent(long idStudent)
        {
            RepositoryModel<List<CourseModel>> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new List<CourseModel>(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _courseServices.GetListCourseOfStudent(idStudent);
            return result;
        }

        /// <summary>
        /// GetListUserNameRegisterForCourse
        /// </summary>
        /// <param name="idCourse"></param>
        /// <returns></returns>
        [HttpGet("student-name-of-course/{idCourse}")]
        public async Task<RepositoryModel<List<string?>>> GetListUserNameRegisterForCourse(long idCourse)
        {
            RepositoryModel<List<string?>> result = new()
            {
                PartnerCode = Messenger.SuccessFull,
                RetCode = ERetCode.Successfull,
                Data = new List<string?>(),
                SystemMessage = string.Empty,
                StatusCode = (int)HttpStatusCode.Created
            };

            result.Data = await _studentServices.GetListUserNameRegisterForCourse(idCourse);
            return result;
        }
    }
}