using Microsoft.AspNetCore.Mvc;

namespace SixteenClothing.App.Controllers
{
    public class ProductController : Controller
    {

        public async Task<IActionResult> Index()
        {

            return View();
        }
    }
}
