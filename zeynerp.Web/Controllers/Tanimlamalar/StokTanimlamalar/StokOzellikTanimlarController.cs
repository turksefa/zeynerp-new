using Microsoft.AspNetCore.Mvc;

namespace zeynerp.Web.Controllers.Tanimlamalar.StokTanimlamalar
{
    [Route("[controller]")]
    public class StokOzellikTanimlarController : Controller
    {
        private readonly ILogger<StokOzellikTanimlarController> _logger;

        public StokOzellikTanimlarController(ILogger<StokOzellikTanimlarController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}