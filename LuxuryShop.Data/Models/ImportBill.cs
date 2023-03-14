using System;
using System.Collections.Generic;

namespace LuxuryShop.Data.Models
{
    public partial class ImportBill
    {
        public ImportBill()
        {
            ImportBillDetails = new HashSet<ImportBillDetail>();
        }

        public int ImportBillId { get; set; }
        public string? BillNumber { get; set; }
        public DateTime? Date { get; set; }
        public int? UserId { get; set; }
        public int? SupplierId { get; set; }

        public virtual Supplier? Supplier { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<ImportBillDetail> ImportBillDetails { get; set; }
    }
}
