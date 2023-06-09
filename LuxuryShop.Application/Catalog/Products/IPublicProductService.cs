﻿using LuxuryShop.ViewModels.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxuryShop.Application.Catalog.Products
{
    public interface IPublicProductService
    {
        List<ProductViewModel> GetProductByLanguage(int pageIndex, int pageSize, out long total, string languageId);
        List<ProductViewModel> GetProductWithCate(int pageIndex, int pageSize, out long total,int CatID, string languageId);
        List<GetProductViewModel> GetProductBestSeller(GetProduct request);
        List<GetProductViewModel> GetProductBestBuy(GetProduct request);
        List<GetProductViewModel> GetProductNew(GetProduct request);
        ProductViewModel GetById(int productId, string languageId);
    }
}
