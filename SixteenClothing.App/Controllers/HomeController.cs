using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SixteenClothing.App.Contexts;
using SixteenClothing.App.ViewModels.Home;
using SixteenClothing.App.ViewModels.Slider;

namespace SixteenClothing.App.Controllers
{
    public class HomeController : Controller
    {
        readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var sliders = await _context.Sliders.AsQueryable().Select(slider => new SliderVM
            {
                Id = slider.Id,
                Heading = slider.Heading,
                ImageUrl = slider.ImageUrl,
                Text = slider.Text
            }).ToListAsync();
            var homeVM = new HomeVM { Sliders = sliders };
            return View(homeVM);
        }
    }
}
