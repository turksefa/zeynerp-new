using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace zeynerp.Web.Controllers.Tanimlamalar.StokTanimlamalar
{
    [Authorize]
    public class StokTanimlamalarController : Controller
    {
        [Route("stok-tanimlamalar")]
        public IActionResult Index()
        {
            return View();
        }
    }
}