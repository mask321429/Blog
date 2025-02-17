﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Blog.Data.Models
{
    public class User
    {
        public Guid Id { get; set; }
        [Required]
        [MinLength(1)]
        public string? FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        [Required]
        public string? Gender { get; set; }
        [Phone]
        public string? PhoneNumber { get; set; }
        [Required]
        [MinLength(1)]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [MinLength(6)]
        public string? Password { get; set; }


    }
}
