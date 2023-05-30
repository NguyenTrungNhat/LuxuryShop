using LuxuryShop.Application.Catalog.Cart;
using LuxuryShop.Application.Catalog.Products;
using LuxuryShop.ViewModels.Catalog.Cart;
using LuxuryShop.ViewModels.Catalog.Categories;
using LuxuryShop.ViewModels.Catalog.ExportBill;
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

        [Route("CreateExportBill")]
        [HttpPost()]
        public IActionResult CreateExportBill([FromBody] CreateExportBillViewModels request)
        {
            var result = _publicCartService.CreateExportBill(request);
            if (result == 0)
                return BadRequest();
            return Ok(request);
        }

        [Route("{OrderID}/Update-Status-User")]
        [HttpPut]
        public IActionResult UpdateStatusUser(int OrderID)
        {
            var affectedResult = _publicCartService.UpdateStatusUser(OrderID);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [Route("GetListCartAll")]
        [HttpGet]
        public IActionResult GetListCartAll()
        {
            var listOrder = _publicCartService.GetListCartAll();
            if (listOrder == null)
            {
                return BadRequest("Cannot find Customer");
            }
            return Ok(listOrder);
        }

        [Route("{UserName}/GetEmailUser")]
        [HttpGet]
        public IActionResult GetListCartAll(string UserName)
        {
            var EmailUser = _publicCartService.GetEmailUser(UserName);
            if (EmailUser == null)
            {
                return BadRequest("Cannot find User");
            }
            return Ok(EmailUser);
        }

        [Route("{Email}/GetListCartAll")]
        [HttpGet]
        public IActionResult GetListOrderUser(string Email)
        {
            var listOrderUser = _publicCartService.GetListOrderUser(Email);
            if (listOrderUser == null)
            {
                return BadRequest("Cannot find user");
            }
            return Ok(listOrderUser);
        }

        [HttpGet("{email}/{orderID}/GetListOrderByEmail")]
        public IActionResult GetListOrderByEmail(string email, int orderID)
        {
            var listOrder = _publicCartService.GetListCartByEmail(email, orderID);
            if (listOrder == null)
            {
                return BadRequest("Cannot find Customer");
            }
            return Ok(listOrder);
        }
    }
}
