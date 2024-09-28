using HDNXUdemyModel.Base;

namespace HDNXUdemyModel.Model
{
    public class CategoryModel : BaseModel
    {
        public string? Name { get; set; }

        public string? PictureUrl { get; set; }

        public string? PublicId { get; set; }

        public int? NumberCourse { get; set; }

        public string? Color { get; set; } = "info";
    }
}