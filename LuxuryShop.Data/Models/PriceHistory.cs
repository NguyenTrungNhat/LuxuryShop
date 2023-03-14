using System;
using System.Collections.Generic;

namespace LuxuryShop.Data.Models
{
    public partial class PriceHistory
    {
        public int PriceId { get; set; }
        public int? ProductId { get; set; }
        public DateTime? StartDay { get; set; }
        public DateTime? EndDay { get; set; }
        public double? Price { get; set; }

        public virtual Product? Product { get; set; }
    }
}
