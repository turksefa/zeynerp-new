using Microsoft.AspNetCore.Mvc;

namespace zeynerp.Web.Controllers.Tanimlamalar.MuhasebeTanimlamalar
{
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