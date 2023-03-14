using System;
using System.Collections.Generic;

namespace LuxuryShop.Data.Models
{
    public partial class Supplier
    {
        public Supplier()
        {
            ImportBills = new HashSet<ImportBill>();
        }

        public int SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }

        public virtual ICollection<ImportBill> ImportBills { get; set; }
    }
}
