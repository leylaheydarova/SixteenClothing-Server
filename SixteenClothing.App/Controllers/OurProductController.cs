using Microsoft.AspNetCore.Mvc;
using SixteenClothing.App.Services.Interfaces;
using SixteenClothing.App.ViewModels.Product;

namespace SixteenClothing.App.Controllers
{
    public class OurProductController : Controller
    {
        readonly IProductService _productService;
        readonly ICategoryService _categoryService;

        public OurProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(int? categoryId, int page = 1)
        {
            await SendViewBag();
            var prvm = await _productService.GetAllAsync(categoryId, page, 6);

            var vm = new OurProductVM() { Products = prvm, CurrentPage = page, SelectedCategoryId = categoryId };
            return View(vm);
        }

        async Task SendViewBag()
        {
            ViewBag.Categories = await _categoryService.GetAllEntitiesAsync();
        }
    }
}
