using System;
using System.Collections.Generic;

namespace LuxuryShop.Data.Models
{
    public partial class CheckWhdetail
    {
        public int CheckWhdetailId { get; set; }
        public int? ProductId { get; set; }
        public int? CheckWh { get; set; }
        public int? QuantityCount { get; set; }
        public int? QuantityCalculate { get; set; }
        public int? QuantityChange { get; set; }

        public virtual CheckWh? CheckWhNavigation { get; set; }
        public virtual Product? Product { get; set; }
    }
}
