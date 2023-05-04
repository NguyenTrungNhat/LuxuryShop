using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxuryShop.ViewModels.Catalog.Cart
{
    public class ListOrderViewModel
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime OrderDate { get; set; }
        public string StatusOrder { get; set; }
        public string? ProductName { get; set; }
        public string? ProductImage { get; set; }
        public int? quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? TotalMoney { get; set; }
        public int? OrderID { get; set; }
    }
}
