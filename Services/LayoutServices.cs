using ChapterOne.DataAccessLayer;
using ChapterOne.Interfaces;
using ChapterOne.Models;
using ChapterOne.ViewModels.BasketViewModel;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ChapterOne.Services
{
    public class LayoutServices:ILayoutService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LayoutServices(AppDbContext appDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _appDbContext = appDbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<BasketVM>> GetBaskets()
        {
            string basket = _httpContextAccessor.HttpContext.Request.Cookies["basket"];

            List<BasketVM> basketVMs = null;

            if (!string.IsNullOrWhiteSpace(basket))
            {
                basketVMs = JsonConvert.DeserializeObject<List<BasketVM>>(basket);

                foreach (BasketVM basketVM in basketVMs)
                {
                    Product product = await _appDbContext.Products
                        .FirstOrDefaultAsync(p =>p.Id== basketVM.Id && p.IsDeleted ==false);

                    if (product != null)
                    {
                        basketVM.Price = product.DiscountedPrice > 0 ? product.DiscountedPrice : product.Price;
                        basketVM.Title = product.Title;
                        basketVM.Image = product.Image;
                    }
                }
            }
            else
            {
                basketVMs = new List<BasketVM>();
            }

            return basketVMs;
        }
        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _appDbContext.Categories.Where(c=>c.IsDeleted == false).ToListAsync();
        }

        public async Task<IDictionary<string, string>> GetSettings()
        {
            IDictionary<string,string> settings = await _appDbContext.Settings.ToDictionaryAsync(s=>s.Key,s=>s.Value);

            return settings;
        }
    }
}
