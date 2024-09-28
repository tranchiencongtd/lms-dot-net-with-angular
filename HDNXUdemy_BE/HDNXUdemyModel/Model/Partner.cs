using HDNXUdemyModel.Base;

namespace HDNXUdemyModel.Model
{
    public class PartnerModel : BaseModel
    {
        public string? Name { get; set; }

        public string? PublicId { get; set; }

        public string? PictureUrl { get; set; }
    }
}