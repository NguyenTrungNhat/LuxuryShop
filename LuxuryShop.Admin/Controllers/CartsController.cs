using LuxuryShop.Application.Catalog.Cart;
using LuxuryShop.Application.Catalog.Products;
using LuxuryShop.ViewModels.Catalog.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LuxuryShop.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly IManageCartService _manageCartService;
        public CartsController(IManageCartService manageCartService)
        {
            _manageCartService = manageCartService;
        }

        [HttpGet("{email}/GetListOrderByEmail")]
        public IActionResult GetListOrderByEmail(string email)
        {
            var listOrder = _manageCartService.GetListCartByEmail(email);
            if (listOrder == null)
            {
                return BadRequest("Cannot find Customer");
            }
            return Ok(listOrder);
        }

        [Route("GetListCartAll")]
        [HttpGet]
        public IActionResult GetListCartAll()
        {
            var listOrder = _manageCartService.GetListCartAll();
            if (listOrder == null)
            {
                return BadRequest("Cannot find Customer");
            }
            return Ok(listOrder);
        }

        [Route("{OrderID}/Update-Status")]
        [HttpPut]
        public IActionResult UpdateStatus(int OrderID)
        {
            var affectedResult = _manageCartService.UpdateStatus(OrderID);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }
    }
}
