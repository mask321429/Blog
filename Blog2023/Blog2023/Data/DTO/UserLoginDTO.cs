using System;
using System.ComponentModel.DataAnnotations;

namespace Blog2023.Data.DTO
{
    public class UserLoginDTO
    {
        [Required]
        [MinLength(1)]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [MinLength(1)]
        public string? Password { get; set; }

    }


}
