using Microsoft.AspNetCore.Mvc;

namespace zeynerp.Web.Controllers.Tanimlamalar.StokTanimlamalar
{
    public class StokTanimlamalarController : Controller
    {
        [Route("stok-tanimlamalar")]
        public IActionResult Index()
        {
            return View();
        }
    }
}