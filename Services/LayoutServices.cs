using ChapterOne.DataAccessLayer;
using ChapterOne.Interfaces;
using ChapterOne.Models;
using Microsoft.EntityFrameworkCore;

namespace ChapterOne.Services
{
    public class LayoutServices:ILayoutService
    {
        private readonly AppDbContext _appDbContext;
        public LayoutServices(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
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
