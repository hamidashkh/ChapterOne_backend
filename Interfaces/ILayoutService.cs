using ChapterOne.Models;

namespace ChapterOne.Interfaces
{
    public interface ILayoutService
    {
        Task<IDictionary<string, string>> GetSettings();
        Task<IEnumerable<Category>> GetCategories();
    }
}
