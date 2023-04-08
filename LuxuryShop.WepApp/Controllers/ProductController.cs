using LuxuryShop.Application.Catalog.Products;
using LuxuryShop.ViewModels.Catalog.Products;
using LuxuryShop.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LuxuryShop.WepApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IPublicProductService _publicProductService;

        public ProductController(IPublicProductService publicProductService)
        {
            _publicProductService = publicProductService;
        }

        [Route("getProductByLanguage")]
        [HttpPost]
        public ResponseModel GetProductByLanguage([FromBody] ResponRequestProductLanguage request)
        {
            var response = new ResponseModel();
            try
            {
             
                long total = 0;
                var data = _publicProductService.GetProductByLanguage(request.page, request.pageSize, out total, request.languageId);
                response.TotalItems = total;
                response.Data = data;
                response.Page = request.page;
                response.PageSize = request.pageSize;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return response;
        }

        [Route("getProductByCate")]
        [HttpPost]
        public ResponseModel GetProductByCate([FromBody] ResponRequestByCate request)
        {
            var response = new ResponseModel();
            try
            {

                long total = 0;
                var data = _publicProductService.GetProductWithCate(request.page, request.pageSize, out total,request.CatID, request.languageId);
                response.TotalItems = total;
                response.Data = data;
                response.Page = request.page;
                response.PageSize = request.pageSize;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return response;
        }

        [Route("getProductBestBuy")]
        [HttpPost]
        public List<GetProductViewModel> GetProductBestBuy([FromBody] GetProduct request)
        {
            return _publicProductService.GetProductBestBuy(request);
        }

        [Route("getProductNew")]
        [HttpPost]
        public List<GetProductViewModel> GetProductNew([FromBody] GetProduct request)
        {
            return _publicProductService.GetProductNew(request);
        }

        [Route("getProductBestSeller")]
        [HttpPost]
        public List<GetProductViewModel> GetProductBestSeller([FromBody] GetProduct request)
        {
            return _publicProductService.GetProductBestSeller(request);
        }

        [HttpGet("GetProductDetail/{productId}/{languageId}")]
        public ProductViewModel GetById(int productId, string languageId)
        {
            return _publicProductService.GetById(productId, languageId);
        }
    }
}
