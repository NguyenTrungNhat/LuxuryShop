using LuxuryShop.Data.Helper;
using LuxuryShop.Data.Helper.Interfaces;
using LuxuryShop.Data.Models;
using LuxuryShop.ViewModels.Catalog.Cart;
using LuxuryShop.ViewModels.Catalog.ProductImages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxuryShop.Application.Catalog.Cart
{
    public class ManageCartService : IManageCartService
    {
        private readonly IDatabaseHelper _dbHelper;

        public ManageCartService(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
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

        public int UpdateStatus(int OrderID)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_order_update_status",
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
    }
}
