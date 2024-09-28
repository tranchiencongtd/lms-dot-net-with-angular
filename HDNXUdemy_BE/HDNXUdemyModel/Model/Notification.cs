using HDNXUdemyModel.Base;

namespace HDNXUdemyModel.Model
{
    public class NotificationModel : BaseModel
    {
        public long? IdComment { get; set; }

        public int? TypeNotification { get; set; }

        public long? IdCourse { get; set; }

        public long? IdStudent { get; set; }

        public string? ShortComment { get; set; }

        public bool? IsRead { get; set; }
    }
}