using ChapterOne.DataAccessLayer;
using ChapterOne.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChapterOne.Areas.Manage.Controllers
{
    [Area("manage")]
    public class AuthorController : Controller
    {
        private readonly AppDbContext _context;

        public AuthorController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Author> authors = await _context.Authors
                .Include(a=>a.Products.Where(p=>p.IsDeleted==false))
                .Where(b => b.IsDeleted == false)
                .OrderByDescending(a => a.Id)
                .ToListAsync();

            return View(authors);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Author author)
        {
            if (!ModelState.IsValid)
            {
                return View(author);
            }

            if (await _context.Authors.AnyAsync(a=>a.IsDeleted==false && a.FullName.Trim().ToLower().Contains(author.FullName.Trim().ToLower())))
            {
                ModelState.AddModelError("FullName", "There  is  already author with this FullName.");

                return View(author) ;
            }

            author.FullName= author.FullName.Trim();
            author.CreatedBy = "System";
            author.CreatedAt= DateTime.UtcNow.AddHours(4);

            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
