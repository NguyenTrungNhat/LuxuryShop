using LuxuryShop.Data.Helper;
using LuxuryShop.Data.Helper.Interfaces;
using LuxuryShop.ViewModels.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxuryShop.Application.Catalog.Products
{
    public class PublicProductService : IPublicProductService
    {
        private readonly IDatabaseHelper _dbHelper;

        public PublicProductService(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public List<GetProductViewModel> GetProductBestBuy(GetProduct request)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_Product_GetProductBestBuy",
                    "@Quantity", request.Quantity,
                    "@LanguageId", request.LanguageId);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<GetProductViewModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GetProductViewModel> GetProductBestSeller(GetProduct request)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_Product_GetProductBestSeller",
                    "@Quantity", request.Quantity,
                    "@LanguageId", request.LanguageId);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<GetProductViewModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProductViewModel> GetProductByLanguage(int pageIndex, int pageSize, out long total, string languageId)
        {
            string msgError = "";
            total = 0;
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_product_getByLanguagetoPaging",
                    "@page_index", pageIndex,
                    "@page_size", pageSize,
                    "@languageId",languageId);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                if (dt.Rows.Count > 0) total = (long)dt.Rows[0]["RecordCount"];
                return dt.ConvertTo<ProductViewModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GetProductViewModel> GetProductNew(GetProduct request)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_Product_GetProductNew",
                    "@Quantity", request.Quantity,
                    "@LanguageId", request.LanguageId);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<GetProductViewModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
