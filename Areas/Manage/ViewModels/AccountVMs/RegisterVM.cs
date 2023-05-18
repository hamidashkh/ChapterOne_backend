﻿using System.ComponentModel.DataAnnotations;

namespace ChapterOne.Areas.Manage.ViewModels.AccountVMs
{
    public class RegisterVM
    {
        [StringLength(100)]

        public string? Name { get; set; }
        [StringLength(100)]

        public string? Surname { get; set; }
        [StringLength(100)]

        public string? FatherName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
