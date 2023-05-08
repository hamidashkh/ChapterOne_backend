using ChapterOne.DataAccessLayer;
using ChapterOne.ViewModels.HomeViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChapterOne.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context= context;
        }
        public async Task<IActionResult> Index()
        {
            HomeVM vm = new HomeVM
            {
                Sliders= await _context.Sliders.Where(s=>s.IsDeleted == false).ToListAsync(),
                BestSeller= await _context.Products.Where(p=> p.IsDeleted==false && p.IsBestSeller==true).ToListAsync(),
                NewArrival= await _context.Products.Where(p=>p.IsDeleted==false && p.IsNewArrival==true).ToListAsync(),
            };
            return View(vm);
        }
    }
}
