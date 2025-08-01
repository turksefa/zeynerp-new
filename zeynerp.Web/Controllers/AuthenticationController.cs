using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using zeynerp.Application.Common.Interfaces;
using zeynerp.Application.DTOs;
using zeynerp.Application.DTOs.Authentication;
using zeynerp.Domain.Entities.Identity;
using zeynerp.Web.Models;
using zeynerp.Web.Models.Authentication;

namespace zeynerp.Web.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITenantService _tenantService;
        private readonly IInvitationService _invitationService;

        public AuthenticationController(IAuthenticationService authenticationService,
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ITenantService tenantService,
            IInvitationService invitationService)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tenantService = tenantService;
            _invitationService = invitationService;
        }

        [Route("kayit-ol")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("kayit-ol")]
        public async Task<IActionResult> Register([FromForm] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _authenticationService.RegisterAsync(_mapper.Map<RegisterDto>(model));
            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = "Kaydınız başarıyla tamamlandı. E-posta adresinize gönderilen aktivasyon bağlantısına tıklayarak hesabınızı etkinleştirin.";
                return RedirectToAction("Login");
            }

            ModelState.AddModelError(string.Empty, result.ErrorMessage);

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }

            return View(model);
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            ModelState.Clear();

            var result = await _authenticationService.ConfirmEmailAsync(userId, token);
            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = "Hesabınız başarıyla etkinleştirildi.";
                return RedirectToAction("Login");
            }

            ModelState.AddModelError("", result.ErrorMessage);

            return View("Login");
        }

        [Route("giris-yap")]
        public IActionResult Login([FromQuery] string? returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"]?.ToString();
            }
            return View();
        }

        [HttpPost]
        [Route("giris-yap")]
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

        [HttpPost]
        public IActionResult ExternalLogin(string provider, [FromForm] string? returnUrl)
        {
            returnUrl = returnUrl ?? Url.Content("~/Dashboard/Index");

            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl, string? remoteError = null)
        {
            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Harici sağlayıcı hatası: {remoteError}");
                return View("Login");
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState.AddModelError(string.Empty, "Harici oturum açma bilgileri yüklenirken hata oluştu.");
                return View("Login");
            }

            // External login ile giriş denemesi
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

            if (result.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }

            // if (result.IsLockedOut) ?? Lockout
            // {
            //     return View("Lockout");
            // }
            // else
            // { }

            // Kullanıcı mevcut değilse, yeni kullanıcı oluştur
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            var fullName = info.Principal.FindFirstValue(ClaimTypes.Name);
            var user = new ApplicationUser { UserName = email, Email = email };

            if (fullName != null)
                user.FullName = fullName;

            var createResult = await _userManager.CreateAsync(user);
            if (createResult.Succeeded)
            {
                user.EmailConfirmed = true;
                await _userManager.AddLoginAsync(user, info);
                await _signInManager.SignInAsync(user, isPersistent: false);

                var tenantResult = await _tenantService.CreateTenantDatabaseAsync();
                if (!tenantResult.IsSuccess)
                {
                    await _userManager.DeleteAsync(user);
                    ModelState.AddModelError(string.Empty, "Kayıt işlemi tamamlanamadı. Lütfen tekrar deneyin.");
                    return View("Login");
                }

                user.TenantId = tenantResult.Data;
                await _userManager.UpdateAsync(user);

                // var updateResult = await _userManager.UpdateAsync(user);
                // if (!updateResult.Succeeded)
                // {
                //     await _userManager.DeleteAsync(user);
                //     Delete tenant
                // }

                return LocalRedirect(returnUrl);
            }

            foreach (var error in createResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View("Login");
        }

        [Route("sifre-unuttum")]
        public IActionResult ForgotPassword()
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"]?.ToString();
            }

            if (TempData["ResetPasswordErrorMessage"] != null)
            {
                var errorMessage = TempData["ResetPasswordErrorMessage"]?.ToString();
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    ModelState.AddModelError("", errorMessage);
                }
            }

            return View();
        }

        [HttpPost]
        [Route("sifre-unuttum")]
        public async Task<IActionResult> ForgotPassword([FromForm] ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _authenticationService.ForgotPasswordAsync(_mapper.Map<ForgotPasswordDto>(model));
            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = "Şifre sıfırlama bağlantısı e-posta adresinize gönderildi. E-postanızı kontrol edin.";
                return RedirectToAction("ForgotPassword");
            }

            ModelState.AddModelError("", result.ErrorMessage);

            return View(model);
        }

        [Route("sifre-sifirla")]
        public IActionResult ResetPassword(string userId, string token)
        {
            if (userId == null || token == null)
            {
                TempData["ResetPasswordErrorMessage"] = "Şifre sıfırlama bağlantısı geçersiz veya bozulmuş.";
                return RedirectToAction("ForgotPassword");
            }

            var model = new ResetPasswordViewModel
            {
                UserId = userId,
                Token = token
            };

            return View(model);
        }

        [HttpPost]
        [Route("sifre-sifirla")]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _authenticationService.ResetPasswordAsync(_mapper.Map<ResetPasswordDto>(model));
            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = "Şifreniz başarıyla sıfırlandı. Yeni şifrenizle giriş yapabilirsiniz.";
                return RedirectToAction("Login");
            }

            ModelState.AddModelError(string.Empty, result.ErrorMessage);

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _authenticationService.LogoutAsync();
            return RedirectToAction("Login");
        }

        [Route("davet-kabul")]
        public async Task<IActionResult> AcceptInvitation([FromQuery] Guid invitationId)
        {
            if (invitationId == Guid.Empty)
            {
                ModelState.AddModelError("", "Geçersiz davet bağlantısı.");
                return View("Login");
            }
            
            return View(_mapper.Map<InvitationViewModel>(await _invitationService.GetInvitationByIdAsync(invitationId)));
        }

        [HttpPost]
        [Route("davet-kabul")]
        public async Task<IActionResult> AcceptInvitation(InvitationViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _invitationService.AcceptInvitationAsync(_mapper.Map<InvitationDto>(model));
            if(result.IsSuccess)
            {
                TempData["SuccessMessage"] = "Davet başarıyla kabul edildi. Giriş yapabilirsiniz.";
                return RedirectToAction("Login");
            }

            ModelState.AddModelError(string.Empty, result.ErrorMessage);
            
            return View();
        }
    }
}