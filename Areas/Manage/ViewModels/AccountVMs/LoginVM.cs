using System.ComponentModel.DataAnnotations;

namespace ChapterOne.Areas.Manage.ViewModels.AccountVMs
{
    public class LoginVm
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
