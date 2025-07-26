using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using zeynerp.Application.Services.Subscription;
using zeynerp.Web.Models.Subscription;

namespace zeynerp.Web.Controllers.Subscription
{
    [Authorize]
    public class PlansController : Controller
    {
        private readonly IPlanService _planService;
        private readonly IMapper _mapper;

        public PlansController(IPlanService planService, IMapper mapper)
        {
            _planService = planService;
            _mapper = mapper;
        }

        [Route("planlar")]
        public async Task<IActionResult> Index() => View(_mapper.Map<IEnumerable<PlanViewModel>>(await _planService.GetPlansAsync()));
    }
}