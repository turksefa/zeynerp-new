using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using zeynerp.Application.Common.Interfaces;
using zeynerp.Application.DTOs.Authentication;
using zeynerp.Domain.Entities.Identity;
using zeynerp.Web.Models.Authentication;

namespace zeynerp.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITenantService _tenantService;

        public AccountController(IAuthenticationService authenticationService,
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ITenantService tenantService)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tenantService = tenantService;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _authenticationService.RegisterAsync(_mapper.Map<RegisterDto>(model));
            if (result.IsSuccess)
                return RedirectToAction("Login");

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }

            return View(model);
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token)){}
                // error

            var result = await _authenticationService.ConfirmEmailAsync(userId, token);
            if (result.IsSuccess)
                return RedirectToAction("Login");

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }

            return View(result.Errors);
        }
        
        public IActionResult Login([FromQuery] string? returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginViewModel model, [FromForm] string? returnUrl)
        {
            returnUrl = returnUrl ?? Url.Content("~/Dashboard/Index");
            if (!ModelState.IsValid)
                return View(model);

            var result = await _authenticationService.LoginAsync(_mapper.Map<LoginDto>(model));
            if (result.IsSuccess)
                return Redirect(returnUrl);

            ModelState.AddModelError(string.Empty, result.ErrorMessage);

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _authenticationService.LogoutAsync();
            return RedirectToAction("Login");
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }        

        [HttpPost]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/Dashboard/Index");

            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl, string remoteError = null)
        {
            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"External provider error: {remoteError}");
                return View("Login");
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState.AddModelError(string.Empty, "Error loading external login information.");
                return View("Login");
            }

            // External login ile giriş denemesi
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

            if (result.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }

            if (result.IsLockedOut)
            {
                return View("Lockout");
            }
            else
            {
                // Kullanıcı mevcut değilse, yeni kullanıcı oluştur
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                var fullName = info.Principal.FindFirstValue(ClaimTypes.Name);
                var user = new ApplicationUser { UserName = email, Email = email };
                user.FullName = fullName;

                var createResult = await _userManager.CreateAsync(user);
                if (createResult.Succeeded)
                {
                    user.EmailConfirmed = true;
                    await _userManager.AddLoginAsync(user, info);
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    var tenantId = await _tenantService.CreateTenantDatabaseAsync();

                    user.TenantId = tenantId.Data;
                    await _userManager.UpdateAsync(user);

                    return LocalRedirect(returnUrl);
                }

                foreach (var error in createResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View("Login");
        }
    }
}