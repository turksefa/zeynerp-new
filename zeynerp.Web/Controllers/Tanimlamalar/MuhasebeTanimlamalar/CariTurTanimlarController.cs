using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using zeynerp.Application.DTOs.Tanimlamalar.MuhasebeTanimlamalar;
using zeynerp.Application.Services.Tanimlamalar.MuhasebeTanimlamalar;
using zeynerp.Web.Models.Tanimlamalar.MuhasebeTanimlamalar;

namespace zeynerp.Web.Controllers.Tanimlamalar.MuhasebeTanimlamalar
{
    public class CariTurTanimlarController : Controller
    {
        private readonly ICariTurTanimService _cariTurTanimService;
        private readonly IMapper _mapper;

        public CariTurTanimlarController(ICariTurTanimService cariTurTanimService, IMapper mapper)
        {
            _cariTurTanimService = cariTurTanimService;
            _mapper = mapper;
        }

        [Route("cari-tur-tanimlar")]
        public async Task<IActionResult> Index()
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"]?.ToString();
            }

            return View(_mapper.Map<IEnumerable<CariTurTanimViewModel>>(await _cariTurTanimService.GetAllAsync()));
        }

        [Route("cari-tur-tanimlar/ekle")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("cari-tur-tanimlar/ekle")]
        public async Task<IActionResult> Create([FromForm] IEnumerable<CariTurTanimViewModel> model)
        {
            if (ModelState.IsValid)
            {
                var result = await _cariTurTanimService.AddRangeAsync(_mapper.Map<List<CariTurTanimDto>>(model));

                if (result.IsSuccess)
                {
                    TempData["SuccessMessage"] = result.Data;
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", result.ErrorMessage);
            }

            return View(model);
        }

        public async Task<IActionResult> Edit([FromForm] CariTurTanimViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _cariTurTanimService.UpdateAsync(_mapper.Map<CariTurTanimDto>(model));
                if (result.IsSuccess)
                    TempData["SuccessMessage"] = result.Data;
            }

            return RedirectToAction("Index");
        }
    }
}