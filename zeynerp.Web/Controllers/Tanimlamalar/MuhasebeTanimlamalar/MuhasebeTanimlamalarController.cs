using Microsoft.AspNetCore.Mvc;

namespace zeynerp.Web.Controllers.Tanimlamalar.MuhasebeTanimlamalar
{
    public class MuhasebeTanimlamalarController : Controller
    {
        [Route("muhasebe-tanimlamalar")]
        public IActionResult Index()
        {
            return View();
        }
    }
}