using HDNXUdemyModel.Base;

namespace HDNXUdemyData.Model
{
    public class BannerModel : BaseModel
    {
        public string? UrlPicture { get; set; }

        public string? ContentBanner { get; set; }

        public string? PublicId { get; set; }

        public bool IsActive { get; set; }
        public string? LinkAd { get; set; }
    }
}