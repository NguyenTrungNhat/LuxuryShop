using LuxuryShop.Data.Helper;
using LuxuryShop.Data.Helper.Interfaces;
using LuxuryShop.ViewModels.Catalog.Categories;
using LuxuryShop.ViewModels.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxuryShop.Application.Catalog.Categories
{
    public class PublicCategoriesService : IPublicCategoriesService
    {
        private readonly IDatabaseHelper _dbHelper;

        public PublicCategoriesService(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public List<CategoriesViewModel> GetCategoriesByLanguage(string languageId)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_categories_get_by_languageId",
                    "@LanguageId", languageId);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<CategoriesViewModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
