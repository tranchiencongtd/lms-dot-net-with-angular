using HDNXUdemyModel.Base;

namespace HDNXUdemyModel.Model
{
    public class CourseEvaluationModel : BaseModel
    {
        public long IdStudent { get; set; }

        public long IdCourse { get; set; }

        public int VoteStartNumber { get; set; }

        public string? CommentEvaluation { get; set; }

        public int Like { get; set; }

        public int DisLike { get; set; }

        public UserModel? Users { get; set; }
    }
}