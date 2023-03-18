using LuxuryShop.Application.Catalog.Categories;
using LuxuryShop.Application.Catalog.Products;
using LuxuryShop.ViewModels.Catalog.Categories;
using LuxuryShop.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LuxuryShop.WepApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IPublicCategoriesService _publicCategoriesService;

        public CategoriesController(IPublicCategoriesService publicCategoriesService)
        {
            _publicCategoriesService = publicCategoriesService;
        }

        [Route("getCategoriesByLanguage/{LanguageId}")]
        [HttpGet]
        public IActionResult GetCategoriesByLanguage(string LanguageId)
        {
            List<CategoriesViewModel> result =  _publicCategoriesService.GetCategoriesByLanguage(LanguageId);
            return Ok(result);
        }
    }
}
