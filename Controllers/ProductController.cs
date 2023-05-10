using ChapterOne.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;

namespace ChapterOne.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context=context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Search(string search)
        {
            return Ok(search);
        }
    }
}
