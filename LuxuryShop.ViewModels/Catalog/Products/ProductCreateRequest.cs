using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxuryShop.ViewModels.Catalog.Products
{
    public class ProductCreateRequest
    {
        public int CatID { get; set; }
        public int Discount { get; set; }
        public int UnitslnStock { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public string Description { get; set; }
        public string SeoDescription { get; set; }
        public string Title { get; set; }
        public string SeoTitle { get; set; }
        public string LanguageId { get; set; }

        public string SeoAlias { get; set; }
        public int Price { get; set; }

        //formfile chứa thông tin các ảnh
        public IFormFile? ThumbnailImage { get; set; }

    }
}
