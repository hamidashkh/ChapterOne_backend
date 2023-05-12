using ChapterOne.ViewModels.HeaderViewComponentVM;
using Microsoft.AspNetCore.Mvc;

namespace ChapterOne.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(HeaderVM headerVM)
        {
            return View(await Task.FromResult(headerVM));
        }
    }
}
