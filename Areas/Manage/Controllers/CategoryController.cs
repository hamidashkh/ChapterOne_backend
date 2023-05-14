using ChapterOne.DataAccessLayer;
using ChapterOne.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChapterOne.Areas.Manage.Controllers
{
    [Area("manage")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.Include(c=>c.Products).Where(c => c.IsDeleted == false).ToListAsync()) ;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            if (await _context.Categories.AnyAsync(a => a.IsDeleted == false && a.Name.ToLower().Contains(category.Name.Trim().ToLower())))
            {
                ModelState.AddModelError("Name", "There  is  already category with this Name.");

                return View(category);
            }

            category.Name = category.Name.Trim();
            category.CreatedBy = "System";
            category.CreatedAt = DateTime.UtcNow.AddHours(4);

            await _context.Categories.AddAsync(category);
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

            Category Category = await _context.Categories.FirstOrDefaultAsync(a=>a.IsDeleted==false && a.Id==id);

            if (Category==null)
            {
                return NotFound();
            }

            return View(Category);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id,Category Category)
        {
            if (!ModelState.IsValid) return View(Category);
            

            if (id == null) return BadRequest();

            if (id != Category.Id) return BadRequest();


            Category dbCategory = await _context.Categories.FirstOrDefaultAsync(a => a.IsDeleted == false && a.Id == id);

            if (Category == null)  return NotFound();

            if (await _context.Categories.AnyAsync(a => a.IsDeleted == false && a.Name.Trim().ToLower().Contains(Category.Name.Trim().ToLower()) && Category.Id !=a.Id))
            {
                ModelState.AddModelError("Name", $"There  is  already Category with {Category.Name} Name.");

                return View(Category);
            }

            dbCategory.Name = Category.Name.Trim();
            dbCategory.UpdatedBy = "System";
            dbCategory.UpdatedAt = DateTime.UtcNow.AddHours(4);

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

            Category Category = await _context.Categories.Include(a=>a.Products.Where(p=>p.IsDeleted==false)).FirstOrDefaultAsync(a => a.IsDeleted == false && a.Id == id);

            if (Category == null)
            {
                return NotFound();
            }

            return View(Category);
            
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Category Category = await _context.Categories.Include(a => a.Products.Where(p => p.IsDeleted == false)).FirstOrDefaultAsync(a => a.IsDeleted == false && a.Id == id);

            if (Category == null)
            {
                return NotFound();
            }

            return View(Category);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCategory(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Category Category = await _context.Categories.Include(a => a.Products.Where(p => p.IsDeleted == false)).FirstOrDefaultAsync(a => a.IsDeleted == false && a.Id == id);

            if (Category == null)
            {
                return NotFound();
            }

            Category.IsDeleted= true;
            Category.DeletedBy = "System";
            Category.DeletedAt = DateTime.UtcNow.AddHours(4);

            foreach (Product product in Category.Products)
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
