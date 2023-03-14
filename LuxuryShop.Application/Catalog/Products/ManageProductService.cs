using LuxuryShop.Data.Helper;
using LuxuryShop.Data.Helper.Interfaces;
using LuxuryShop.Utilities.Exceptions;
using LuxuryShop.ViewModels.Catalog.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxuryShop.Application.Catalog.Products
{
    public class ManageProductService : IManageProductService
    {
        private readonly IDatabaseHelper _dbHelper;

        public ManageProductService(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public int Create(ProductCreateRequest request)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_product_create",
                "@CatID", request.CatID,
                "@Discount", request.Discount,
                "@UnitslnStock", request.UnitslnStock,
                "@Name",request.Name,
                "@Details",request.Details,
                "@Description",request.Description,
                "@SeoDescription",request.SeoDescription,
                "@Title",request.Title,
                "@SeoTitle",request.SeoTitle,
                "@LanguageId",request.LanguageId,
                "@SeoAlias",request.SeoAlias,
                "@Price",request.Price);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new LuxuryShopException(Convert.ToString(result) + msgError);
                }
                string msgErrorPr = "";
                try
                {
                    var query = "SELECT TOP(1) ProductID AS Id FROM Products ORDER BY ProductID DESC";
                    DataTable dt = _dbHelper.ExecuteQueryToDataTable(query, out msgErrorPr);
                    List<int> productId = new List<int>();
                    foreach (DataRow row in dt.Rows)
                    {
                        productId.Add((int)row[0]);
                    }
                    return productId[0];
                }
                catch(Exception ex)
                {
                    throw ex;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      
        public int Delete(int productId)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_product_delete",
                "@productId", productId);
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

        public ProductViewModel GetById(int productId, string languageId)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_product_get_by_id",
                     "@productId", productId,
                     "@languageId", languageId);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<ProductViewModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Update(ProductUpdateRequest request)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_product_update",
                "@ProductId", request.Id,
                "@LanguageId", request.LanguageId,
                "@Name", request.Name,
                "@SeoAlias", request.SeoAlias,
                "@SeoDescription", request.SeoDescription,
                "@SeoTitle", request.SeoTitle,
                "@Description",request.Description,
                "@Details",request.Details);
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

        public bool UpdatePrice(int productId, decimal newPrice)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_product_update_price",
                "@ProductId", productId,
                "@newPrice", newPrice);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateStock(int productId, int addedQuantity)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_product_update_stock",
                "@ProductId", productId,
                "@addQuantity",addedQuantity);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
