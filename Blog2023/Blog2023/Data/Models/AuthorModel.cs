using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.Data.Models
{
    public class AuthorModel
    {
        [Key] 
        public Guid id { get; set; }
        [Required]
        [MinLength(1)]
        public string FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        [Required]
        public string Gender { get; set; }

        [Required]
        public int Posts { get; set; }
        [Required]
        public int Likes { get ; set; }

        public DateTime? Created { get; set; }

    }


}
