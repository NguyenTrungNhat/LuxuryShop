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
    }
}
