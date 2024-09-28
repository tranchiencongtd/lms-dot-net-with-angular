namespace HDNXUdemyData.Entities
{
    public class CourseEvaluationEntities : BaseEntities
    {
        public long IdStudent { get; set; }

        public long IdCourse { get; set; }

        public int VoteStartNumber { get; set; }

        public string? CommentEvaluation { get; set; }

        public int Like { get; set; }

        public int DisLike { get; set; }
    }
}