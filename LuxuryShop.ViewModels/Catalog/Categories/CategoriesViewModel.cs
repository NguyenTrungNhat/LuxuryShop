using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxuryShop.ViewModels.Catalog.Categories
{
    public class CategoriesViewModel
    {
        public int CatID { get; set; }
        public int ParentID { get; set; }
        public int SortOrder { get; set; }
        public bool IsShowOnHome { get; set; }
        public int Status { get; set; }
        public string SeoDescription { get; set; }
        public string SeoTitle { get; set; }
        public string LanguageId { get; set; }
        public string SeoAlias { get; set; }
        public string Name { get; set; }
    }
}
