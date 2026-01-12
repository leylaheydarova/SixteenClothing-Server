using Microsoft.AspNetCore.Mvc;

namespace SixteenClothing.App.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
