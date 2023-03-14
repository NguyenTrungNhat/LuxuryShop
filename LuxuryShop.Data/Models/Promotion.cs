using System;
using System.Collections.Generic;

namespace LuxuryShop.Data.Models
{
    public partial class Promotion
    {
        public Promotion()
        {
            PromotionsDetails = new HashSet<PromotionsDetail>();
        }

        public int PromotionsId { get; set; }
        public string? PromotionsName { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<PromotionsDetail> PromotionsDetails { get; set; }
    }
}
