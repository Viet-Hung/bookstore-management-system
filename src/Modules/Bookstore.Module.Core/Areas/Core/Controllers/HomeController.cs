using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Module.Core.Areas.Core.Controllers
{
    [Area("Core")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}