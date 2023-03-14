using LuxuryShop.Application.Catalog.Products;
using LuxuryShop.ViewModels.Catalog.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LuxuryShop.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IManageProductService _manageProductService;

        public ProductController(IManageProductService manageProductService)
        {
            _manageProductService = manageProductService;
        }

        [HttpGet("{productId}/{languageId}")]
        public ProductViewModel GetById(int productId, string languageId)
        {
            return _manageProductService.GetById(productId, languageId);
        }

        [HttpPost]
        public IActionResult Create([FromForm] ProductCreateRequest request)
        {
            var productId = _manageProductService.Create(request);
            if (productId == 0)
                return BadRequest();
            //return Ok(request);

            var product = _manageProductService.GetById(productId, request.LanguageId);
            return Created(nameof(GetById), product);
        }

        [HttpDelete("{productId}")]
        public IActionResult Delete(int productId)
        {
            var affectedResult = _manageProductService.Delete(productId);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [Route("Update-Product")]
        [HttpPut]
        public IActionResult Update([FromForm] ProductUpdateRequest request)
        {
            var affectedResult =  _manageProductService.Update(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpPut("Update-Stock/{productId}/{addedQuantity}")]
        public IActionResult UpdateStock(int productId,int addedQuantity)
        {
            var affectedResult = _manageProductService.UpdateStock(productId,addedQuantity);
            if (!affectedResult)
                return BadRequest();
            return Ok();
        }

        [HttpPut("Update-Price/{productId}/{newPrice}")]
        public IActionResult UpdatePrice(int productId, decimal newPrice)
        {
            var affectedResult = _manageProductService.UpdatePrice(productId, newPrice);
            if (!affectedResult)
                return BadRequest();
            return Ok();
        }
    }
}
