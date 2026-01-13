using Microsoft.AspNetCore.Mvc;
using SixteenClothing.App.Services.Interfaces;
using SixteenClothing.App.ViewModels.Slider;

namespace SixteenClothing.App.Areas.admin.Controllers
{
    [Area("admin")]
    public class SliderController : Controller
    {
        readonly ISliderService _service;

        public SliderController(ISliderService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(int page, int size)
        {
            return View(await _service.GetAllAsync(page, size));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);
            await _service.CreateAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var slider = await _service.GetSingleAsync(id);
            var vm = new SliderUpdateVM()
            {
                Id = slider.Id,
                Heading = slider.Heading,
                ImageName = slider.ImageName,
                ImageUrl = slider.ImageUrl,
                Text = slider.Text
            };
            return View(vm);
        }

        [HttpPost("edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SliderUpdateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);
            await _service.UpdateAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("remove")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int id)
        {
            ;
            await _service.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
