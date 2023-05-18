﻿using Microsoft.AspNetCore.Identity;
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

    }
}
