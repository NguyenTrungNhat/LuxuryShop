using LuxuryShop.Application.Common;
using LuxuryShop.Data.Helper.Interfaces;
using LuxuryShop.Utilities.Exceptions;
using LuxuryShop.ViewModels.Catalog.Cart;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LuxuryShop.Application.Catalog.Cart
{
    public class PublicCartService : IPublicCartService
    {
        private readonly IDatabaseHelper _dbHelper;

        public PublicCartService(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public int CreateOrder(CreateDonHangViewModel request)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_order_create",
                "@khach", JsonSerializer.Serialize(request.Customer),
                "@listchitiet", JsonSerializer.Serialize(request.OrderDetails));
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new LuxuryShopException(Convert.ToString(result) + msgError);
                }
                return 1;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
