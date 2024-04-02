using System.ComponentModel.DataAnnotations;

namespace Blog2023.Data.DTO
{
    public class CommentDTO
    {
        public Guid Id { get; set; }
        [Required]
        [MinLength(1)]
        public string content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? modifiedDate { get; set; }
        public DateTime? deleteDate { get; set; }
        [Required]
        public Guid authorId { get; set; }
        [Required]
        [MinLength(1)]
        public string author { get; set; }
        [Required]
        public Int32 subComments { get; set; }
    }
}
