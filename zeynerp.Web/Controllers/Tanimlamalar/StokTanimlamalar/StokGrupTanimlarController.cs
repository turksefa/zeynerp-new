using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using zeynerp.Application.DTOs.Tanimlamalar.StokTanimlamalar;
using zeynerp.Application.Services.Tanimlamalar.StokTanimlamalar;
using zeynerp.Web.Models.Tanimlamalar.StokTanimlamalar;

namespace zeynerp.Web.Controllers.Tanimlamalar.StokTanimlamalar
{
    public class StokGrupTanimlarController : Controller
    {
        private readonly IStokGrupTanimService _stokGrupTanimService;
        private readonly IMapper _mapper;

        public StokGrupTanimlarController(IStokGrupTanimService stokGrupTanimService, IMapper mapper)
        {
            _stokGrupTanimService = stokGrupTanimService;
            _mapper = mapper;
        }

        [Route("stok-grup-tanimlar")]
        public async Task<IActionResult> Index()
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"]?.ToString();
            }

            return View(_mapper.Map<IEnumerable<StokGrupTanimViewModel>>(await _stokGrupTanimService.GetAllAsync()));
        }

        [Route("stok-grup-tanimlar/ekle")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("stok-grup-tanimlar/ekle")]
        public async Task<IActionResult> Create([FromForm] IEnumerable<StokGrupTanimViewModel> model)
        {
            if (ModelState.IsValid)
            {
                var result = await _stokGrupTanimService.AddRangeAsync(_mapper.Map<List<StokGrupTanimDto>>(model));

                if (result.IsSuccess)
                {
                    TempData["SuccessMessage"] = result.Data;
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", result.ErrorMessage);
            }

            return View(model);
        }

        public async Task<IActionResult> Edit([FromForm] StokGrupTanimViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _stokGrupTanimService.UpdateAsync(_mapper.Map<StokGrupTanimDto>(model));
                if (result.IsSuccess)
                    TempData["SuccessMessage"] = result.Data;
            }

            return RedirectToAction("Index");
        }
    }
}