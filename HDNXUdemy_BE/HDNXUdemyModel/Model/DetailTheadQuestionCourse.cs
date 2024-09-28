using HDNXUdemyModel.Base;

namespace HDNXUdemyModel.Model
{
    public class DetailTheadQuestionCourseModel : BaseModel
    {
        public long IdTheadQuestionCourse { get; set; }

        public long IdStudent { get; set; }

        public string? Comment { get; set; }

        public int Like { get; set; }

        public int DisLike { get; set; }
    }
}