using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace zeynerp.Web.Controllers.Tanimlamalar.MuhasebeTanimlamalar
{
    [Authorize]
    public class MuhasebeTanimlamalarController : Controller
    {
        [Route("muhasebe-tanimlamalar")]
        public IActionResult Index()
        {
            return View();
        }
    }
}