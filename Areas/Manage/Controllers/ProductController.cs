using ChapterOne.DataAccessLayer;
using ChapterOne.Extentions;
using ChapterOne.Helpers;
using ChapterOne.Models;
using ChapterOne.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChapterOne.Areas.Manage.Controllers
{
    [Area("manage")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
         private readonly IWebHostEnvironment _env;

        public ProductController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;

        }
        [HttpGet]
        public async Task<IActionResult> Index(int pageindex=1)
        {
            IQueryable<Product> products = _context.Products.Where(p => p.IsDeleted == false);

            return View(PageNatedList<Product>.Create(products, pageindex, 3));

        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Author = await _context.Authors.Where(b => b.IsDeleted == false).ToListAsync();
            ViewBag.Categories = await _context.Categories
                .Where(b => b.IsDeleted == false)
                .ToListAsync();

            
            return View();
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            ViewBag.Author = await _context.Authors.Where(b => b.IsDeleted == false).ToListAsync();
            ViewBag.Categories = await _context.Categories
                .Where(b => b.IsDeleted == false)
                .ToListAsync();

            if (!ModelState.IsValid) return View(product);

            if (!await _context.Authors.AnyAsync(a => a.IsDeleted == false && a.Id == product.AuthorId)) 
            {
                ModelState.AddModelError("AuthorId", "Daxil edilen Id yanlisdir");
                return View(product);
            }
            
            if (!await _context.Categories.AnyAsync(a => a.IsDeleted == false && a.Id == product.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "Daxil edilen Id yanlisdir");
                return View(product);
            }

            if(product.ImageFile !=null)
            {
                if (!product.ImageFile.CheckFileContentType("image/jpeg"))
                {
                    ModelState.AddModelError("ImageFile", "Image File formati yanlisdir(*.jpeg)");
                    return View(product);
                }
                if (!product.ImageFile.CheckFileLength(300))
                {
                    ModelState.AddModelError("ImageFile", "Image File ölçüsü maksimum 300kb ola biler");
                    return View(product);
                }
                product.Image = await product.ImageFile.CreateFileAsync(_env, "assets", "images", "product");
            }
            else
            {
                ModelState.AddModelError("ImageFile", "Image File Secilmelidir");
                return View(product);
            }
            product.CreatedAt= DateTime.UtcNow.AddHours(4);
            product.CreatedBy = "System";
              
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();  

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return BadRequest();

            Product product = await _context.Products.FirstOrDefaultAsync(p => p.IsDeleted == false && p.Id == id);

            if (product == null) return NotFound();


            ViewBag.Author = await _context.Authors.Where(b => b.IsDeleted == false).ToListAsync();
            ViewBag.Categories = await _context.Categories
                .Where(b => b.IsDeleted == false)
                .ToListAsync();

            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Product product)
        {
            ViewBag.Authors = await _context.Authors.Where(b => b.IsDeleted == false).ToListAsync();
            ViewBag.Categories = await _context.Categories
                .Where(b => b.IsDeleted == false)
                .ToListAsync();


            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id == null) return BadRequest();

            if (id != product.Id) return BadRequest();


            Product dbProduct = await _context.Products
                .FirstOrDefaultAsync(p => p.IsDeleted == false && p.Id == id);

            if (dbProduct == null) return NotFound();
            if (product.ImageFile != null)
            {
                if (!product.ImageFile.CheckFileContentType("image/jpeg"))
                {
                    ModelState.AddModelError("MainFile", "Main File Yalniz JPG Olmalidir");
                    return View(product);
                }

                if (!product.ImageFile.CheckFileLength(300))
                {
                    ModelState.AddModelError("ImageFile", "Main File Yalniz 300 kb Olmalidir");
                    return View(product);
                }

                FileHelper.DeleteFile(dbProduct.Image, _env, "assets", "images", "product");

                dbProduct.Image = await product.ImageFile.CreateFileAsync(_env, "assets", "images", "product");
            }
            dbProduct.Title = product.Title;
            dbProduct.Description = product.Description;
            dbProduct.Category = product.Category;
            dbProduct.Author= product.Author;   
            dbProduct.UpdatedBy = "System";
            dbProduct.UpdatedAt = DateTime.UtcNow.AddHours(4);
            dbProduct.Count = product.Count;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            ViewBag.Authors = await _context.Authors.Where(b => b.IsDeleted == false).ToListAsync();
            ViewBag.Categories = await _context.Categories
                .Where(b => b.IsDeleted == false)
                .ToListAsync();
            if (id == null)
            {
                return BadRequest();
            }

            Product product = await _context.Products.Include(p => p.Author).Include(p=>p.Category).FirstOrDefaultAsync(a => a.IsDeleted == false && a.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);

        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Product product = await _context.Products.FirstOrDefaultAsync(a => a.IsDeleted == false && a.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if (id == null) return BadRequest();

            Product product = await _context.Products
                .FirstOrDefaultAsync(c => c.Id == id && c.IsDeleted == false);

            if (product == null) return NotFound();
            

            product.IsDeleted = true;
            product.DeletedAt = DateTime.UtcNow.AddHours(4);
            product.DeletedBy = "System";

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


    }
}

    


    

