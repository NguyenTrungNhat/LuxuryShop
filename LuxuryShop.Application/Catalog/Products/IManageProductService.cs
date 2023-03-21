using LuxuryShop.ViewModels.Catalog.ProductImages;
using LuxuryShop.ViewModels.Catalog.Products;
using Microsoft.AspNetCore.Http;
using LuxuryShop.ViewModels.Catalog.ProductImages;


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
        List<ProductViewModel> GetAllPaging(int pageIndex, int pageSize, out long total);
        string SaveFile(IFormFile file, string folder);
        int AddImage(int productId, ProductImageCreateRequest request);
        int RemoveImage(int imageId);
        int UpdateImage(int imageId, ProductImageUpdateReques request);
        ProductImageViewModel GetImageById(int imageId);
        List<ProductImageViewModel> GetListImages(int productId);

    }
}
