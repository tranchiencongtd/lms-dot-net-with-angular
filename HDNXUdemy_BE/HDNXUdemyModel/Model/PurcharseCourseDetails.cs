using HDNXUdemyModel.Base;
using HDNXUdemyModel.Model;

namespace HDNXUdemyData.Entities
{
    public class PurcharseCourseDetailsModel : BaseModel
    {
        public long IdCourse { get; set; }

        public long IdStudent { get; set; }

        public decimal? PriceOfCourse { get; set; }

        public decimal? PriceOfDiscount { get; set; }

        public long IdPurchaseOrder { get; set; }

        public CourseModel? Courses { get; set; }
    }
}