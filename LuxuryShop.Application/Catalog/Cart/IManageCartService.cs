using LuxuryShop.ViewModels.Catalog.Cart;
using LuxuryShop.ViewModels.Catalog.ProductImages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxuryShop.Application.Catalog.Cart
{
    public interface IManageCartService
    {
        List<ListOrderViewModel> GetListCartByEmail(string email, int orderID);
        List<ListCustomerOrderViewModel> GetListCartAll();
        int UpdateStatus(int OrderID);
    }
}
