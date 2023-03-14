using System;
using System.Collections.Generic;

namespace LuxuryShop.Data.Models
{
    public partial class ImportBillDetail
    {
        public int Ibdid { get; set; }
        public int? ProductId { get; set; }
        public int? ImportBillId { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }

        public virtual ImportBill? ImportBill { get; set; }
        public virtual Product? Product { get; set; }
    }
}
