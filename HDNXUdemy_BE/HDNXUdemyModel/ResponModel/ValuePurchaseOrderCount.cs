using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDNXUdemyModel.ResponModel
{
    public class ValuePurchaseOrderCount
    {
        public string? KeyValue { get; set; }

        public int UpOrDown { get; set; } // 0 : Up, 1 : Down

        public decimal? PercentBasePreviewMonth { get; set; }

        public decimal? ValueOfCurrent { get; set; }
    }
}
