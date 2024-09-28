using HDNXUdemyModel.Constant;
using NodaTime;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HDNXUdemyData.Entities
{
    public class BaseEntities
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public long Id { get; set; }

        [Column("CreateDate")]
        public LocalDateTime CreateDate { get; set; }

        [Column("UpdateDate")]
        public LocalDateTime UpdateDate { get; set; }

        [Column("CreateBy")]
        public long CreateBy { get; set; }

        [Column("UpdateBy")]
        public long UpdateBy { get; set; }

        [Column("Status")]
        [DefaultValue((int)EStatus.Active)]
        public int? Status { get; set; }
    }
}