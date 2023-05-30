using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxuryShop.Data.Models
{
    public partial class ExportBillDetail
    {
        public int ID { get; set; }
        public int? ProductID { get; set; }
        public int? ExportBillID { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }
        public double? Discount { get; set; }
        public int Status { get; set; }
    }
}
