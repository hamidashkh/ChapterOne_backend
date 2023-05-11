using ChapterOne.Models;
using ChapterOne.ViewModels.BasketViewModel;

namespace ChapterOne.Interfaces
{
    public interface ILayoutService
    {
        Task<IDictionary<string, string>> GetSettings();
        Task<IEnumerable<Category>> GetCategories();
        Task<IEnumerable<BasketVM>> GetBaskets();
    }
}
