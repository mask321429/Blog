using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.Data.Models
{
    public class TagModel
    {
        [Required]
        public string Name { get; set; }
        public Guid Id { get; set; }
        [Required]
        [MinLength(1)]
   
        public DateTime? CreateTime { get; set; }
     
    }
}
