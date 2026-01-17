using Microsoft.AspNetCore.Mvc;
using SixteenClothing.App.Areas.admin.ViewModels.Category;
using SixteenClothing.App.Constants;
using SixteenClothing.App.Services.Interfaces;


namespace SixteenClothing.App.Areas.admin.Controllers
{
    [Area(nameof(Area.Admin))]
    public class CategoryController : Controller
    {
        readonly IService<CategoryGetVM, CategoryGetVM, CategoryCreateVM, CategoryUpdateVM> _service;

        public CategoryController(IService<CategoryGetVM, CategoryGetVM, CategoryCreateVM, CategoryUpdateVM> service)
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
        public async Task<IActionResult> Create(CategoryCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);
            await _service.CreateAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("edit-category/{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            var category = await _service.GetSingleAsync(id);
            var editVm = new CategoryUpdateVM()
            {
                Id = category.Id,
                Name = category.Name,
            };
            return View(editVm);
        }

        [HttpPost("edit-category/{id}")]
        public async Task<IActionResult> Edit(CategoryUpdateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);
            await _service.UpdateAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("remove/{id}")]
        public async Task<IActionResult> Remove([FromRoute] int id)
        {
            await _service.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
