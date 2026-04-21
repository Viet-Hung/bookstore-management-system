using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Module.Core.Areas.Core.Controllers
{
    [Area("Core")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}