using ChapterOne.Models;
using ChapterOne.ViewModels.BasketViewModel;

namespace ChapterOne.ViewModels.HeaderViewComponentVM
{
    public class HeaderVM
    {
        public IDictionary<string, string> Settings { get;set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<BasketVM> BasketVMs { get; set; }
    }
}
