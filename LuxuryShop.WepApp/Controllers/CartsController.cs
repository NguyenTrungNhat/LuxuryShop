using LuxuryShop.Application.Catalog.Cart;
using LuxuryShop.Application.Catalog.Products;
using LuxuryShop.ViewModels.Catalog.Cart;
using LuxuryShop.ViewModels.Catalog.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LuxuryShop.WepApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly IPublicCartService _publicCartService;

        public CartsController(IPublicCartService publicCartService)
        {
            _publicCartService = publicCartService;
        }

        [Route("CreateOrder")]
        [HttpPost()]
        public IActionResult Create([FromBody] CreateDonHangViewModel request)
        {
            var result = _publicCartService.CreateOrder(request);
            if (result == 0)
                return BadRequest();
            return Ok(request);
        }

    }
}
