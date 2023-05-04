using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxuryShop.ViewModels.Catalog.Cart
{
    public class ListCustomerOrderViewModel
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public string StatusOrder { get; set; }
        public bool Deleted { get; set; }
        public bool Paid { get; set; }
        public decimal TotalMoney { get; set; }
        public string Note { get; set; }
    }
}
