namespace HDNXUdemyData.Entities
{
    public class BannerEntities : BaseEntities
    {
        public string? UrlPicture { get; set; }

        public string? ContentBanner { get; set; }

        public string? PublicId { get; set; }

        public bool IsActive { get; set; }

        public string? LinkAd { get; set; }
    }
}