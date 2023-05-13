using System.ComponentModel.DataAnnotations;

namespace ChapterOne.Models
{
    public class Author:BaseEntity
    {
        [StringLength(255)]
        [Required]
        public string FullName { get; set; }
        
        public IEnumerable<Product>? Products { get; set; } 
    }
}
