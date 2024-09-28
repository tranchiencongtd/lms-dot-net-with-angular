using HDNXUdemyModel.Base;

namespace HDNXUdemyModel.Model
{
    public class StudentPromotionModel : BaseModel
    {
        public long IdCourse { get; set; }

        public long IdStudent { get; set; }

        public long IdPromotion { get; set; }

        public string? CodePromotion { get; set; }

        public int ValuePromotion { get; set; }
    }
}