using LuxuryShop.Application.Catalog.Products;
using LuxuryShop.Application.Common;
using LuxuryShop.Data.Models;
using LuxuryShop.ViewModels.Catalog.Products;
using LuxuryShop.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using LuxuryShop.ViewModels.Catalog.ProductImages;

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

        [Route("getAllPaging")]
        [HttpPost]
        public ResponseModel GetAllPaging([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseModel();
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                long total = 0;
                var data = _manageProductService.GetAllPaging(page, pageSize, out total);
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

        //Image
        [HttpPost("{productId}/images")]
        public IActionResult CreateImage(int productId, [FromForm] ProductImageCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var imageId = _manageProductService.AddImage(productId, request);
            if (imageId == 0)
                return BadRequest();

            var image = _manageProductService.GetImageById(imageId);
            return Created(nameof(GetImageById), image);
        }

        [HttpPut("/images/{imageId}")]
        public IActionResult UpdateImage(int imageId, [FromForm] ProductImageUpdateReques request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _manageProductService.UpdateImage(imageId, request);
            if (result == 0)
                return BadRequest();
            return Ok();


        }

        [HttpDelete("/images/{imageId}")]
        public IActionResult RemoveImage(int imageId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _manageProductService.RemoveImage(imageId);
            if (result == 0)
                return BadRequest();
            return Ok();


        }

        [HttpGet("/image/{imageId}")]
        public ProductImageViewModel GetImageById(int imageId)
        {
            return _manageProductService.GetImageById(imageId);
        }

        [HttpGet("{productId}/image/GetListImage")]
        public IActionResult GetListImage(int productId)
        {
            var image =  _manageProductService.GetListImages(productId);
            if (image == null)
            {
                return BadRequest("Cannot find product");
            }
            return Ok(image);
        }

    }
}
