using NodaTime;

namespace HDNXUdemyModel.Base
{
    public class BaseModel
    {
        public long? Id { get; set; }

        public long CreateBy { get; set; }

        public long UpdateBy { get; set; }

        public LocalDateTime? CreateDate { get; set; }

        public LocalDateTime? UpdateDate { get; set; }

        public int Status { get; set; }
    }
}