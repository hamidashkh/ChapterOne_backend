using Microsoft.AspNetCore.Mvc;

namespace ChapterOne.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            return View();
        }
    }
}
