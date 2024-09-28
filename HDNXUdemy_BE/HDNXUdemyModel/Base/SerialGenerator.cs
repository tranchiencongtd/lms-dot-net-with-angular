using System.ComponentModel.DataAnnotations;

namespace HDNXUdemyModel.Base
{
    public class SerialGenerator
    {
        [Key]
        public long Id { get; set; }

        [StringLength(50)]
        public string? SerialPart { get; set; }

        public int NumericPart { get; set; } = 0;

        [StringLength(254)]
        public string? TypeName { get; set; }
    }
}