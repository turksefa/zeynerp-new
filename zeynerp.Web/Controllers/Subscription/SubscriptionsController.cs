using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using zeynerp.Application.DTOs.Subscription;
using zeynerp.Application.Services.Subscription;
using zeynerp.Web.Models.Subscription;

namespace zeynerp.Web.Controllers.Subscription
{
    [Authorize]
    public class SubscriptionsController : Controller
    {
        private readonly IPlanSubscriptionService _planSubscriptionService;
        private readonly IMapper _mapper;

        public SubscriptionsController(IPlanSubscriptionService planSubscriptionService, IMapper mapper)
        {
            _planSubscriptionService = planSubscriptionService;
            _mapper = mapper;
        }

        [Route("abonelikler")]
        public async Task<IActionResult> Index()
        {
            var result = _mapper.Map<IEnumerable<PlanSubscriptionViewModel>>(await _planSubscriptionService.GetSubscriptionsAsync());
            return View(result);
        }

        public async Task<IActionResult> Create([FromQuery] Guid planId)
        {
            ModelState.Clear();

            if (planId == Guid.Empty)
                ModelState.AddModelError(string.Empty, "Geçersiz istek.");

            PlanSubscriptionViewModel planSubscriptionViewModel = new PlanSubscriptionViewModel
            {
                PlanId = planId
            };

            await _planSubscriptionService.CreateSubscriptionAsync(_mapper.Map<PlanSubscriptionDto>(planSubscriptionViewModel));

            return View("Index");
        }
    }
}