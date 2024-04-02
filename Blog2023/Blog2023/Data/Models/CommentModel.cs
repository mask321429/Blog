using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Blog.Data.Models;

namespace Blog2023.Data.Models
{
    public class CommentModel
    {
        public Guid Id { get; set; }
        [Required]
        public DateTime CreatedTime { get; set; }
        [Required]
        [MinLength(1)]
        public string Content { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        [Required]
        public Guid AuthorId { get; set; }
        [Required]
        [MinLength(1)]
        public string? Author { get; set; }
       
        public int SubComments { get; set; }
        public Guid? ParentId { get; set; }
        [ForeignKey("Posts")]
        public Guid PostId { get; set; }


    }
}
