using Microsoft.AspNetCore.Mvc;

namespace ChapterOne.Areas.Manage.Controllers
{
    [Area("manage")]

    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
