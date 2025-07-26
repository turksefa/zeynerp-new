using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace zeynerp.Web.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        [Route("kullanicilar")]
        public IActionResult Index()
        {
            return View();
        }
    }
}