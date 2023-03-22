using LuxuryShop.Application.SystemUser.Users;
using LuxuryShop.ViewModels.SystemUser.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LuxuryShop.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public IActionResult Authenticate([FromForm] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultToken =  _userService.Authenticate(request);
            if (string.IsNullOrEmpty(resultToken))
            {
                return BadRequest("Username or Password is in correct.");
            }
            return Ok(new { token = resultToken });
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult Register([FromForm] RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _userService.Register(request);
            if (!result)
            {
                return BadRequest("Register is  unsuccessful.");
            }
            return Ok();
        }

    }
}
