using ChapterOne.DataAccessLayer;
using ChapterOne.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            IEnumerable<Product> products= await _context.Products.Where(p=>p.IsDeleted==false  &&
            (p.Title.ToLower().Contains(search.ToLower())|| 
            p.Author.Name.ToLower().Contains(search.ToLower()) || p.Author.Surname.ToLower().Contains(search.ToLower()))).ToListAsync();

            return PartialView("_SearchPartial",products);
        }
    }
}
