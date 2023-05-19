using ChapterOne.DataAccessLayer;
using ChapterOne.Models;
using ChapterOne.ViewModels;
using ChapterOne.ViewModels.ProductViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChapterOne.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public ProductController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index(int pageIndex=1)
        {
            IQueryable<Product> products = _context.Products.Include(p=>p.Author).Where(p=>p.IsDeleted==false);
            return  View(PageNatedList<Product>.Create(products,pageIndex,6));
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id,Review review)
        {

            ViewBag.Authors = await _context.Authors.Where(b => b.IsDeleted == false).ToListAsync();
            ViewBag.Reviews = await _context.Reviews.Where(b => b.IsDeleted == false).ToListAsync();
            ViewBag.Categories = await _context.Categories
                .Where(b => b.IsDeleted == false)
                .ToListAsync();

            if (id == null) return BadRequest();

            Product product = await _context.Products
                .Include(p => p.Author)
                .Include(p => p.Category)
                .Include(p => p.Reviews.Where(r => r.IsDeleted == false))
                .FirstOrDefaultAsync(p => p.IsDeleted == false && p.Id == id);

            if (product == null) return NotFound();

            ProductReviewVM productReviewVM = new ProductReviewVM
            {
                Product = product,
                Review = new Review { ProductId=id }
            };

            return View(productReviewVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReview(Review review)
        {
            Product product = await _context.Products
               .Include(p => p.Author)
               .Include(p => p.Category)
               .Include(p => p.Reviews.Where(r => r.IsDeleted == false))
               .FirstOrDefaultAsync(p => p.IsDeleted == false && p.Id == review.ProductId);

            ProductReviewVM productReviewVM = new ProductReviewVM { Product = product, Review = review };

            if (!ModelState.IsValid) return View("Detail",productReviewVM );

            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);

            if(product.Reviews !=null && product.Reviews.Count()>0 && product.Reviews.Any(r=>r.UserId== appUser.Id))
            {
                return View("Detail", productReviewVM);
            }
            review.CreatedBy = $"{appUser.Name} {appUser.Surname}";
            review.CreatedAt = DateTime.UtcNow.AddHours(4);

            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Detail), new { id = product.Id });
            
        }

        public async Task<IActionResult> Search(string search)
        {
           
            IEnumerable<Product> products = await _context.Products.Include(p=>p.Author).Where(p => p.IsDeleted == false &&
            (p.Title.ToLower().Contains(search.ToLower()) ||
            p.Author.FullName.ToLower().Contains(search.ToLower()))).ToListAsync();

            return PartialView("_SearchPartial", products);
        }
    }
}
