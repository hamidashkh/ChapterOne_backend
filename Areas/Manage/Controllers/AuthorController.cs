using ChapterOne.DataAccessLayer;
using ChapterOne.Models;
using ChapterOne.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

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
        public async Task<IActionResult> Index(int pageIndex=1)
        {
            IQueryable<Author> authors = _context.Authors
                .Include(a => a.Products.Where(p => p.IsDeleted == false))
                .Where(b => b.IsDeleted == false)
                .OrderByDescending(a => a.Id);

           

            

            return View( PageNatedList<Author>.Create(authors,pageIndex,3));
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

            if (await _context.Authors.AnyAsync(a=>a.IsDeleted==false && a.FullName.ToLower().Contains(author.FullName.Trim().ToLower())))
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

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Author author = await _context.Authors.FirstOrDefaultAsync(a=>a.IsDeleted==false && a.Id==id);

            if (author==null)
            {
                return NotFound();
            }

            return View(author);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id,Author author)
        {
            if (!ModelState.IsValid) return View(author);
            

            if (id == null) return BadRequest();

            if (id != author.Id) return BadRequest();


            Author dbauthor = await _context.Authors.FirstOrDefaultAsync(a => a.IsDeleted == false && a.Id == id);

            if (author == null)  return NotFound();

            if (await _context.Authors.AnyAsync(a => a.IsDeleted == false && a.FullName.Trim().ToLower().Contains(author.FullName.Trim().ToLower()) && author.Id !=a.Id))
            {
                ModelState.AddModelError("FullName", $"There  is  already author with {author.FullName} FullName.");

                return View(author);
            }

            dbauthor.FullName = author.FullName.Trim();
            dbauthor.UpdatedBy = "System";
            dbauthor.UpdatedAt = DateTime.UtcNow.AddHours(4);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Author author = await _context.Authors.Include(a=>a.Products.Where(p=>p.IsDeleted==false)).FirstOrDefaultAsync(a => a.IsDeleted == false && a.Id == id);

            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin,Admin")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Author author = await _context.Authors.Include(a => a.Products.Where(p => p.IsDeleted == false)).FirstOrDefaultAsync(a => a.IsDeleted == false && a.Id == id);

            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin,Admin")]

        public async Task<IActionResult> DeleteAuthor(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Author author = await _context.Authors.Include(a => a.Products.Where(p => p.IsDeleted == false)).FirstOrDefaultAsync(a => a.IsDeleted == false && a.Id == id);

            if (author == null)
            {
                return NotFound();
            }

            author.IsDeleted= true;
            author.DeletedBy = "System";
            author.DeletedAt = DateTime.UtcNow.AddHours(4);

            foreach (Product product in author.Products)
            {
                product.IsDeleted = true;
                product.DeletedBy = "System";
                product.DeletedAt = DateTime.UtcNow.AddHours(4);
            }
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
