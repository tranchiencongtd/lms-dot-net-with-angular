using NodaTime;

namespace HDNXUdemyModel.RequestModel
{
    public class CouponPromotionCode
    {
        public long? Id { get; set; }
        public decimal PercentOff { get; set; }

        public string? Currency { get; set; } = "USD";

        public string? TypeDuration { get; set; } = "forever";

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int Status { get; set; }

        public string? NameOfCoupon { get; set; }
    }
}