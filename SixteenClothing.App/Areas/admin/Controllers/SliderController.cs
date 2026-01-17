using Microsoft.AspNetCore.Mvc;
using SixteenClothing.App.Areas.admin.ViewModels.Slider;
using SixteenClothing.App.Constants;
using SixteenClothing.App.Services.Interfaces;

namespace SixteenClothing.App.Areas.admin.Controllers
{
    [Area(nameof(Area.Admin))]
    public class SliderController : Controller
    {
        readonly IService<SliderGetVM, SliderGetVM, SliderCreateVM, SliderUpdateVM> _service;

        public SliderController(IService<SliderGetVM, SliderGetVM, SliderCreateVM, SliderUpdateVM> service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(int page = 1, int size = 15)
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

        [HttpGet("edit-slider/{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            var slider = await _service.GetSingleAsync(id);
            var vm = new SliderUpdateVM()
            {
                Id = slider.Id,
                Heading = slider.Heading,
                Text = slider.Text
            };
            return View(vm);
        }

        [HttpPost("edit-slider/{id}")]
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
