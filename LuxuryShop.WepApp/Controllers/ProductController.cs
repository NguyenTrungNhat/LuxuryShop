using LuxuryShop.Application.Catalog.Products;
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
        public ResponseModel GetProductByLanguage([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseModel();
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string languageId = "";
                if (formData.Keys.Contains("languageId") && !string.IsNullOrEmpty(Convert.ToString(formData["languageId"]))) { languageId = Convert.ToString(formData["languageId"]); }
                long total = 0;
                var data = _publicProductService.GetProductByLanguage(page, pageSize, out total, languageId);
                response.TotalItems = total;
                response.Data = data;
                response.Page = page;
                response.PageSize = pageSize;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return response;
        }
    }
}
