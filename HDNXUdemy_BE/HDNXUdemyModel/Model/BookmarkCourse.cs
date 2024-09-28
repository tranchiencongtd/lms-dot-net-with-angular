using HDNXUdemyModel.Base;

namespace HDNXUdemyModel.Model
{
    public class BookmarkCourseModel : BaseModel
    {
        public long IdStudent { get; set; }
        public long IdCourse { get; set; }
    }
}