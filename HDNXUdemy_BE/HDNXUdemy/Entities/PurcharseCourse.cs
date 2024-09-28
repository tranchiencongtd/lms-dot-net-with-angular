using NodaTime;

namespace HDNXUdemyData.Entities
{
    public class PurcharseCourseEntities : BaseEntities
    {
        public long IdStudent { get; set; }

        public string? ContentTranferBanking { get; set; }

        public decimal? TotalPrice { get; set; }

        public decimal? DiscountAmount { get; set; }

        public int PurcharseStatus { get; set; }

        public Guid PurcharseCode { get; set; }

        public LocalDateTime? PurchaseDate { get; set; }

        public string? PaymentIntent { get; set; }

        public string? PromotionCode { get; set; }

        public string? CoupponCode { get; set; }

        public string? CheckoutSessionId { get; set; }
    }
}