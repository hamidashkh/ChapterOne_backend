﻿using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace ChapterOne.Models
{
    public class Order:BaseEntity
    {
        public int No { get; set; }
        public string? UserId { get; set; }
        public AppUser? User { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set;}
        [StringLength(100)]
        public string? Name { get; set; }
        [StringLength(100)]
        public string? SurName { get; set; }
        [StringLength(100)]
        [EmailAddress]
        public string? Email { get; set; }
        [StringLength(10)]
        public string? Phone { get; set; }
        [StringLength(100)]
        public string? Country { get; set; }
        [StringLength(100)]
        public string? State { get; set; }
        [StringLength(100)]
        public string? City { get; set; }
        [StringLength(100)]
        public string? PostalCode { get; set; }
        [StringLength(100)]
        public string? AddressLine { get; set; }
    }
}
