using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace zeynerp.Web.Controllers.Tanimlamalar
{
    [Authorize]
    public class TanimlamalarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}