using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxuryShop.Data.Models
{
    public partial class ExportBill
    {
        public int ID { get; set; }
        public string? BillNumber { get; set; }
        public DateTime? Date { get; set; }
        public int? UserId { get; set; }
        public int? CustomerID { get; set; }
    }
}
