using Microsoft.AspNetCore.Mvc;

namespace zeynerp.Web.Controllers.Tanimlamalar
{
    public class TanimlamalarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}