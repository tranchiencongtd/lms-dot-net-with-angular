using HDNXUdemyModel.Model;

namespace HDNXUdemyServices.IServices
{
    public interface IStudentServices
    {
        Task<bool> CreateStudent(UserModel model);

        Task<bool> UpdateStatusStudent(long id, UserModel model);

        Task<bool> UpdateStudent(long id, UserModel model);

        Task<List<UserModel>> GetListStudents();

        Task<UserModel> GetStudent(long id);

        Task<bool> CreateStudentPromotions(StudentPromotionModel model);

        Task<bool> UpdateStatusStudentPromotions(long id, StudentPromotionModel model);

        Task<bool> UpdateInformationStudentPromotions(long id, StudentPromotionModel model);

        Task<List<StudentPromotionModel>> GetListStudentPromotions();

        Task<StudentPromotionModel> GetStudentPromotions(long id);

        Task<bool> CreateStudentProcess(StudentProcessModel model);

        Task<bool> UpdateStatusStudentProcess(long id, StudentProcessModel model);

        Task<bool> UpdateInformationStudentProcess(long id, StudentProcessModel model);

        Task<List<StudentProcessModel>> GetListStudentProcess();

        Task<StudentProcessModel> GetStudentProcess(long id);

        Task<bool> CreateStudentBookmarkCourse(BookmarkCourseModel model);

        Task<bool> UpdateStatusStudentBookmarkCourse(long id, BookmarkCourseModel model);

        Task<List<CourseModel>> GetListStudentBookmarkCourse(long idUser);

        Task<bool> DeleteStudentBookmarkCourse(long id);

        Task<List<UserModel>> GetListUserManager();

        Task<List<UserModel>> GetListTeachers();

        Task<List<string?>> GetListUserNameRegisterForCourse(long idCourse);
    }
}