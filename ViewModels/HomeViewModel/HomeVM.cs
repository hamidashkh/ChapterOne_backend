using ChapterOne.Models;
using System.Security.Cryptography.X509Certificates;

namespace ChapterOne.ViewModels.HomeViewModel
{
    public class HomeVM
    {
        public IEnumerable<Slider>Sliders { get; set; }
        public IEnumerable<Product> BestSeller  { get; set; }
        public IEnumerable<Product> NewArrival  { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Author> Authors { get; set; }
        public IEnumerable<Team> Teams { get; set; }
    }
}
