using System.ComponentModel.DataAnnotations;

namespace ChapterOne.Models
{
    public class Team:BaseEntity
    {
        public string FullName { get; set; }
        public string Profession { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
