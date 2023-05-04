using LuxuryShop.ViewModels.Catalog.Cart;
using LuxuryShop.ViewModels.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxuryShop.Application.Catalog.Cart
{
    public interface IPublicCartService
    {
        int CreateOrder(CreateDonHangViewModel request);
        List<ListCustomerOrderViewModel> GetListCartAll();
        int UpdateStatusUser(int OrderID);
        string GetEmailUser(string UserName);
        List<ListOrderViewModel> GetListOrderUser(string Email);
        List<ListOrderViewModel> GetListCartByEmail(string email);

    }
}
