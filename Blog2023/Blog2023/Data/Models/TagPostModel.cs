using System.ComponentModel.DataAnnotations;

namespace Blog.Data.Models
{
    public class TagPost
    {
        [Key]
        public Guid id { get; set; }
        public Guid IdPost { get; set; }
        public Guid IdTeg { get; set; }
    }
}
