using LuxuryShop.Application.Common;
using LuxuryShop.Data.Helper;
using LuxuryShop.Data.Helper.Interfaces;
using LuxuryShop.Data.Models;
using LuxuryShop.Utilities.Exceptions;
using LuxuryShop.ViewModels.Catalog.Cart;
using LuxuryShop.ViewModels.Catalog.Products;
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

        public int UpdateStatusUser(int OrderID)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_order_update_status_user",
                "@OrderID", OrderID);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ListCustomerOrderViewModel> GetListCartAll()
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_Carts_getAll");
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<ListCustomerOrderViewModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetEmailUser(string UserName)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_Cart_getEmail_User",
               "@UserName", UserName);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return (string)result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ListOrderViewModel> GetListOrderUser(string Email)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_Cart_getOrder_Use",
                    "@Email",Email);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<ListOrderViewModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ListOrderViewModel> GetListCartByEmail(string email)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_Carts_get_by_email",
                     "@Email", email);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<ListOrderViewModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
