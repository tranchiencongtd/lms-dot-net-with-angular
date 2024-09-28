using HDNXUdemyModel.Model;
using HDNXUdemyModel.ResponModel;
using Microsoft.AspNetCore.Http;

namespace HDNXUdemyServices.IServices
{
    public interface ICourseServices
    {
        Task<CourseModel> CreateCourse(CourseModel model);

        Task<bool> UpdateStatusCourse(long id, int status, int processCourse);

        Task<bool> UpdateInformationCourse(long id, CourseModel model);

        Task<List<StudentProcessModel>> GetListStudentProcess();

        Task<StudentProcessModel> GetStudentProcess(long id);

        Task<bool> AddCommentOfStudentForCourse(CourseEvaluationModel model);

        Task<List<CourseEvaluationModel>> GetListCoursEvaluation(long idCourse);

        Task<bool> UpdateStatusCommentCourse(long id, CourseEvaluationModel model);

        Task<bool> UpdateInformationCommentCourse(long id, CourseEvaluationModel model);

        Task<CourseEvaluationModel> GetCommentCourse(long id);

        Task<bool> CreateContentCourse(ContentCourseModel model);

        Task<bool> UpdateStatusContentCourse(long id, ContentCourseModel model);

        Task<bool> UpdateInformationContentCourse(long id, ContentCourseModel model);

        Task<List<ListContentWithDetailCourse>> GetListContentCourse(long idCourse);

        Task<ContentCourseModel> GetContentCourse(long id);

        Task<bool> CreateContentCourseDetails(ContentCourseDetailModel model);

        Task<bool> UpdateStatusContentCourseDetails(long id, ContentCourseDetailModel model);

        Task<bool> UpdateInformationContentCourseDetails(long id, ContentCourseDetailModel model);

        Task<List<ContentCourseDetailModel>> GetListContentCourseDetails(long idContent);

        Task<ContentCourseDetailModel> GetContentCourseDetails(long id, HttpRequest request);

        Task<bool> CreateTheadQuestionCourse(TheadQuestionCourseModel model);

        Task<bool> UpdateStatusTheadQuestionCourse(long id, TheadQuestionCourseModel model);

        Task<bool> UpdateInformationTheadQuestionCourse(long id, TheadQuestionCourseModel model);

        Task<List<TheadQuestionCourseModel>> GetListTheadQuestionCourse(long idCourse);

        Task<TheadQuestionCourseModel> GetTheadQuestionCourse(long id);

        Task<List<CourseModel>> GetListCourseForAdmin();

        Task<GetCourseWithDetailsContent> GetDetailCourse(long id, bool isAdmin);

        Task<List<CourseModel>> GetListCourseAsCategory(long idCategory);

        Task<List<CourseModel>> GetListCourseOfStudent(long idStudent);

        Task<bool> LikeForCommentCourse(long id);

        Task<bool> DisLikeForCommentCourse(long id);

        Task<bool> CreateDetailsTheadQuestionCourse(DetailTheadQuestionCourseModel model);

        Task<bool> UpdateStatusDetailsTheadQuestionCourse(long id, DetailTheadQuestionCourseModel model);

        Task<bool> UpdateInformationDetailsTheadQuestionCourse(long id, DetailTheadQuestionCourseModel model);

        Task<List<DetailTheadQuestionCourseModel>> GetListDetailsTheadQuestionCourse(long idThear);
    }
}