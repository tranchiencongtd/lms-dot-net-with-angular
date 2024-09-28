namespace HDNXUdemyData.Entities
{
    public class TheadQuestionCourseEntities : BaseEntities
    {
        public long IdStudent { get; set; }

        public long IdCourse { get; set; }

        public string? Comment { get; set; }

        public int Like { get; set; }

        public int DisLike { get; set; }
    }
}