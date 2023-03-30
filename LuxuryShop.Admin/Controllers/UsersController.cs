using LuxuryShop.Application.Catalog.Products;
using LuxuryShop.Application.SystemUser.Users;
using LuxuryShop.ViewModels.Catalog.Products;
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
        private readonly IManageProductService _manageProductService;

        public UsersController(IUserService userService, IManageProductService manageProductService)
        {
            _userService = userService;
            _manageProductService = manageProductService;
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
        public IActionResult Register([FromBody] RegisterRequest request)
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

        [Route("upload")]
        [HttpPost]
        public async Task<string> Upload(IFormFile file)
        {
            var filePath = _manageProductService.SaveFile(file, "Users");
            return filePath;
        }


        [Route("update-user")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Update([FromBody] UpdateUserRequest request)
        {
            var affectedResult = _userService.Update(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpDelete("delete-user/{UserID}")]
        public IActionResult Delete(int UserID)
        {
            var affectedResult = _userService.Delete(UserID);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpGet("get-by-id/{UserID}")]
        [AllowAnonymous]
        public GetUserIdRequest GetById(int UserID)
        {
            return _userService.GetById(UserID);
        }



    }
}
