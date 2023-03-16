using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxuryShop.ViewModels.Catalog.Products
{
    public class ProductImageViewModel
    {
        public int ListProductImageID { get; set; }
        public string ImagePath { get; set; }
        public bool IsDefault { get; set;}
        public long FileSize { get; set; }

    }
}
