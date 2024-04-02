using System;
using System.ComponentModel.DataAnnotations;

namespace Blog2023.Data.DTO
{
	public class CreateNewAuthor
	{
        public string FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        [Required]
        public string Gender { get; set; }

        [Required]
        public int Posts { get; set; }
        [Required]
        public int Likes { get; set; }

        public DateTime? Created { get; set; }
    }
}

