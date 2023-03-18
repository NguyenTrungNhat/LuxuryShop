using LuxuryShop.ViewModels.Catalog.Categories;
using LuxuryShop.ViewModels.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxuryShop.Application.Catalog.Categories
{
    public interface IPublicCategoriesService
    {
        List<CategoriesViewModel> GetCategoriesByLanguage(string languageId);
    }
}
