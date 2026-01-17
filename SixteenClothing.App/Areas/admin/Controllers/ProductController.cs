using Microsoft.AspNetCore.Mvc;
using SixteenClothing.App.Areas.admin.ViewModels.Category;
using SixteenClothing.App.Areas.admin.ViewModels.Product;
using SixteenClothing.App.Constants;
using SixteenClothing.App.Services.Interfaces;

namespace SixteenClothing.App.Areas.admin.Controllers
{
    [Area(nameof(Area.Admin))]
    public class ProductController : Controller
    {
        readonly IService<ProductGetVM, ProductGetVM, ProductCreateVM, ProductUpdateVM> _service;
        readonly IService<CategoryGetVM, CategoryGetVM, CategoryCreateVM, CategoryUpdateVM> _categoryService;
        public ProductController(IService<ProductGetVM, ProductGetVM, ProductCreateVM, ProductUpdateVM> service, IService<CategoryGetVM, CategoryGetVM, CategoryCreateVM, CategoryUpdateVM> categoryService)
        {
            _service = service;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(int page = 1, int size = 15)
        {
            return View(await _service.GetAllAsync(page, size));
        }

        public async Task<IActionResult> Create()
        {
            await SendViewBag();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                await SendViewBag();
                return View(vm);
            }
            await _service.CreateAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("remove-product/{id}")]
        public async Task<IActionResult> Remove([FromRoute] int id)
        {
            await _service.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("edit-product/{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            var product = await _service.GetSingleAsync(id);
            var vm = new ProductUpdateVM()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Image = null
            };
            return View(vm);
        }

        [HttpPost("edit-product/{id}")]
        public async Task<IActionResult> Edit(ProductUpdateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);
            await _service.UpdateAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        async Task SendViewBag()
        {
            ViewBag.Categories = await _categoryService.GetAllAsync();
        }
    }
}
