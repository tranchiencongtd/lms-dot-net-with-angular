using HDNXUdemyData.Entities;
using HDNXUdemyModel.Base;
using HDNXUdemyModel.ResponModel;
using NodaTime;

namespace HDNXUdemyModel.Model
{
    public class PurcharseCourseModel : BaseModel
    {
        public long IdStudent { get; set; }

        public string? ContentTranferBanking { get; set; }

        public decimal? TotalPrice { get; set; }

        public decimal? DiscountAmount { get; set; }

        public int PurcharseStatus { get; set; }

        public Guid PurcharseCode { get; set; }

        public LocalDateTime? PurchaseDate { get; set; }

        public string? NameStatus { get; set; }

        public UserModel? User { get; set; }

        public List<PurcharseCourseDetailsModel>? ListPurchaseCourseDetails { get; set; }

        public InformationManualBankingModel? InfoBanking { get; set; }

        public List<ValuePurchaseOrderCount>? ValueOfDataCount { get; set; }

        public string? PaymentIntent { get; set; }

        public string? PromotionCode { get; set; }

        public string? CoupponCode { get; set; }

        public string? CheckoutSessionId { get; set; }
    }
}