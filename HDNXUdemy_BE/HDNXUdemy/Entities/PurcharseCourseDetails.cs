namespace HDNXUdemyData.Entities
{
    public class PurcharseCourseDetailsEntities : BaseEntities
    {
        public long IdCourse { get; set; }

        public long IdStudent { get; set; }

        public decimal? PriceOfCourse { get; set; }

        public decimal? PriceOfDiscount { get; set; }

        public long IdPurchaseOrder { get; set; }
    }
}