using System.ComponentModel.DataAnnotations;

namespace ChapterOne.ViewModels.AccountViewModel
{
    public class LoginVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RemindMe { get; set; }

    }
}
