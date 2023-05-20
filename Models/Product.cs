using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace ChapterOne.Models
{
    public class Product:BaseEntity
    {
        [StringLength(255)]
        public string Title { get; set; }
        [Column(TypeName = "money")]

        public int Price { get; set; }
        [Column(TypeName = "money")]
        public int DiscountedPrice { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        public int Count { get; set; }
        public bool IsBestSeller { get; set; }
        public bool IsNewArrival { get; set; }
        [StringLength(255)]
        
        public string? Image { get; set; }
        public IEnumerable<Review>? Reviews { get; set; }
        public IEnumerable<Basket>? Baskets { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public int AuthorId { get; set; }
        public Author? Author { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }

    }
}
