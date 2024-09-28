using HDNXUdemyModel.Base;
using Stripe;

namespace HDNXUdemyData.Entities
{
    public class PromotionCodeModel : BaseModel
    {
        public string? IdCoupon { get; set; }
        public string? StripePromotionCodeId { get; set; }

        public string? Object { get; set; }

        public string? Customer { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public bool? Livemode { get; set; }
        public decimal? MaxRedemptions { get; set; }
        public Dictionary<string, string>? Metadata { get; set; }

        public long? TimesRedeemed { get; set; }

        public PromotionCodeRestrictions? Restrictions { get; set; }
    }
}