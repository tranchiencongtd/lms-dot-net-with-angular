using HDNXUdemyModel.Model;

namespace HDNXUdemyModel.ResponModel
{
    public class GetCourseWithDetailsContent : CourseModel
    {
        public string? FileUploadUrlStream { get; set; }

        public List<ContentAndContentDetail>? ListContentCourseDetails { get; set; }

        public List<CourseModel>? ListCourseRate { get; set; }

        public UserModel? Author { get; set; }
    }
}