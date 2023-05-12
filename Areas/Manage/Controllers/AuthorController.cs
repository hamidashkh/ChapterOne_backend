using Microsoft.AspNetCore.Mvc;

namespace ChapterOne.Areas.Manage.Controllers
{
    [Area("manage")]
    public class AuthorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
