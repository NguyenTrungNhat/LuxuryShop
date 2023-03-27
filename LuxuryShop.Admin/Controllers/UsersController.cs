using LuxuryShop.Application.SystemUser.Users;
using LuxuryShop.ViewModels.Common;
using LuxuryShop.ViewModels.SystemUser.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LuxuryShop.Admin.Controllers
{
    [Authorize]
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
        public IActionResult Authenticate([FromBody] LoginRequest request)
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
            return Ok(resultToken);
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


        //https://localhost/api/users/getUsersPaging?page=1&pageSize=1
        [Route("getUsersPaging")]
        [HttpPost]
        public ResponseModel GetAllPaging([FromBody] ResponseRequestBase request)
        {
            var response = new ResponseModel();
            try
            {
                long total = 0;
                var data = _userService.GetUsersPaging(request.page, request.pageSize, out total);
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

    }
}
