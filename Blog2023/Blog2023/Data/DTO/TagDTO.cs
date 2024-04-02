using System;
using System.ComponentModel.DataAnnotations;

namespace Blog2023.Data.DTO
{
    public class TagDTO
    {
        [Required]
        public string Name { get; set; }
        public Guid Id { get; set; }
        [Required]
        [MinLength(1)]
   
        public DateTime? CreateTime { get; set; }

    }
}
