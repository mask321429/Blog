using System.ComponentModel.DataAnnotations;

namespace Blog2023.Data.DTO
{
    public class CreateCommentDTO
    {
        [Required]
        [MinLength(1)]
        public string Content { get; set; }
        public Guid? ParentId { get; set; }
    }
}
