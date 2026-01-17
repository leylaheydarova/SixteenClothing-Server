using Microsoft.AspNetCore.Mvc;
using SixteenClothing.App.Constants;

namespace SixteenClothing.App.Areas.admin.Controllers
{
    [Area(nameof(Area.Admin))]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
