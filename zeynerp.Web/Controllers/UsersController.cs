using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using zeynerp.Application.Services;
using zeynerp.Web.Models;

namespace zeynerp.Web.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("kullanicilar")]
        public async Task<IActionResult> Index()
        {
            InvitationViewModel invitationViewModel = new InvitationViewModel
            {
                Users = await _userService.GetAllUsersAsync()
            };

            return View(invitationViewModel);
        }

        public IActionResult Edit()
        {
            return View();
        }
    }
}