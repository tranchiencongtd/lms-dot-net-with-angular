using HDNXUdemyModel.Base;
using NodaTime;

namespace HDNXUdemyData.Model
{
    public class CouponModel : BaseModel
    {
        public string? NameOfCoupon { get; set; }
        public string? StripeCouponId { get; set; }

        public string? Object { get; set; }

        public decimal? AmountOff { get; set; }

        public string? Currency { get; set; }

        public string? Duration { get; set; }

        public int? DurationInMonths { get; set; }

        public bool? Livemode { get; set; }

        public int? MaxRedemptions { get; set; }

        public string? Metadata { get; set; }

        public string? Name { get; set; }

        public decimal? PercentOff { get; set; }

        public int? RedeemBy { get; set; }

        public decimal? TimesRedeemed { get; set; }

        public bool? Valid { get; set; }

        public DateTime? Created { get; set; }

        public LocalDateTime? StartDate { get; set; }

        public LocalDateTime? EndDate { get; set; }
    }
}