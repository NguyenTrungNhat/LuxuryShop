using System;
using System.Collections.Generic;

namespace LuxuryShop.Data.Models
{
    public partial class PromotionsDetail
    {
        public int PromotionsDetailId { get; set; }
        public int? ProductId { get; set; }
        public DateTime? StartDay { get; set; }
        public DateTime? EndDay { get; set; }
        public int? PromotionsId { get; set; }
        public bool? Status { get; set; }

        public virtual Product? Product { get; set; }
        public virtual Promotion? Promotions { get; set; }
    }
}
