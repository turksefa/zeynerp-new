using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using zeynerp.Application.Services.Products;

namespace zeynerp.Web.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IProductService _productService;

        public DashboardController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index() => View(await _productService.GetProductsAsync());
    }
}