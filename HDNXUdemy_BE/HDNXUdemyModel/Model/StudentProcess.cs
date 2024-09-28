using HDNXUdemyModel.Base;

namespace HDNXUdemyModel.Model
{
    public class StudentProcessModel : BaseModel
    {
        public long IdCourse { get; set; }

        public int NumberContentOfCourse { get; set; }

        public int LastContentOfContent { get; set; } // Bài học sau cùng để resumn
    }
}