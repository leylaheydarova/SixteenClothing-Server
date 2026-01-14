using Microsoft.AspNetCore.Mvc;
using SixteenClothing.App.Services.Interfaces;
using SixteenClothing.App.ViewModels.Home;

namespace SixteenClothing.App.Controllers
{
    public class HomeController : Controller
    {
        readonly ISliderService _service;

        public HomeController(ISliderService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var sliders = await _service.GetAllAsync();
            var homeVM = new HomeVM { Sliders = sliders };
            return View(homeVM);
        }
    }
}
