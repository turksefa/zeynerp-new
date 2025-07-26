using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using zeynerp.Application.Common.Interfaces;
using zeynerp.Application.DTOs;
using zeynerp.Web.Models;

namespace zeynerp.Web.Controllers
{
    public class InvitationsController : Controller
    {
        private readonly IInvitationService _invitationService;
        private readonly IMapper _mapper;

        public InvitationsController(IInvitationService invitationService, IMapper mapper)
        {
            _invitationService = invitationService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] InvitationViewModel model)
        {
            if(!ModelState.IsValid)
                return View(model);

            var result = await _invitationService.SendInvitationAsync(_mapper.Map<InvitationDto>(model));
            if(!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage);
                return View(model);
            }

            TempData["SuccessMessage"] = "Davet e-postası başarıyla gönderildi.";

            return RedirectToAction("Index", "Users");
        }
    }
}