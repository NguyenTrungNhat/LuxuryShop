using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxuryShop.ViewModels.Catalog.Products
{
    public class ProductViewModel
    {
        public int ProductID { get; set; }
        public int CatID { get; set; }
        public int Discount { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public bool BestSellers { get; set; }
        public bool HomeFlag { get; set; }
        public bool Active { get; set; }
        public string Title { get; set; }
        public int UnitsInStock { set; get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
        public string SeoDescription { get; set; }
        public string SeoTitle { get; set; }
        public string LanguageId { get; set; }

        public string SeoAlias { get; set; }
        public string? ImagePath { get; set; }
        public double? Price { get; set; }
    }
}
