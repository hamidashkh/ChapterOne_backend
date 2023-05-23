using Microsoft.AspNetCore.Mvc;

namespace ChapterOne.Controllers
{
    public class EventsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
