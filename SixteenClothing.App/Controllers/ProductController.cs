using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SixteenClothing.App.Contexts;

namespace SixteenClothing.App.Controllers
{
    public class ProductController : Controller
    {
        readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? categoryId, int page = 1)
        {
            await SendViewBag();
            var products = _context.Products.AsNoTracking().AsQueryable();
            if (categoryId.HasValue) products = products.Where(p => p.CategoryId == categoryId);
            return View();
        }

        async Task SendViewBag()
        {
            ViewBag.Categories = await _context.Categories.AsNoTracking().ToListAsync();
        }
    }
}
