using LuxuryShop.ViewModels.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxuryShop.Application.Catalog.Products
{
    public interface IManageProductService
    {
        int Create(ProductCreateRequest request);

        int Update(ProductUpdateRequest request);

        int Delete(int productId);
        ProductViewModel GetById(int productId, string languageId);
        bool UpdatePrice(int productId, decimal newPrice);
        bool UpdateStock(int productId, int addedQuantity);

        //Task<PageResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request);
        //Task<int> AddImage(int productId, List<IFormFile> files);
        //Task<int> RemoveImage(int imageId);
        //Task<int> UpdateImage(int imageId, string caption, bool IsDefault);
        //Task<List<ProductImageViewModel>> GetListImage(int productId);
    }
}
