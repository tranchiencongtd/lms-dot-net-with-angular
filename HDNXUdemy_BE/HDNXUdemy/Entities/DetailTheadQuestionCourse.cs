namespace HDNXUdemyData.Entities
{
    public class DetailTheadQuestionCourseEntities : BaseEntities
    {
        public long IdTheadQuestionCourse { get; set; }

        public long IdStudent { get; set; }

        public string? Comment { get; set; }

        public int Like { get; set; }

        public int DisLike { get; set; }
    }
}