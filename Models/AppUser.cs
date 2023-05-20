using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ChapterOne.Models
{
    public class AppUser : IdentityUser
    {
        [StringLength(100)]
        
        public string? Name { get; set; }
        [StringLength(100)]

        public string? Surname { get; set; }
        [StringLength(100)]

        public string? FatherName { get; set; }
       
        public IEnumerable<Review>? Reviews { get; set; }
        public IEnumerable<Address>? Addresses { get; set; }
        public IEnumerable<Order>? Orders { get; set; }
        public List<Basket>? Baskets { get; set; }



    }
}
