using LuxuryShop.Application.Catalog.Products;
using LuxuryShop.Application.Catalog.Roles;
using LuxuryShop.Data.Models;
using LuxuryShop.ViewModels.Catalog.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LuxuryShop.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IManageRoleService _manageRoletService;


        public RolesController(IManageRoleService manageRoleService)
        {
            _manageRoletService = manageRoleService;
        }

        [HttpGet("GetAll")]
        public List<Role> GetAll()
        {
            return _manageRoletService.GetAll();
        }
    }
}
