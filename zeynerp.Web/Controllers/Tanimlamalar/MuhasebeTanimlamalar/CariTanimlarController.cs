using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace zeynerp.Web.Controllers.Tanimlamalar.MuhasebeTanimlamalar
{
    [Authorize]
    public class CariTanimlarController : Controller
    {
        [Route("cari-tanimlar")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("cari-tanimlar/ekle")]
        public IActionResult Create()
        {
            return View();
        }
    }
}