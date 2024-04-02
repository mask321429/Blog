using System.ComponentModel.DataAnnotations;

namespace Blog2023.Data.DTO
{
    public class CommunityListDto
    {
        [Required]
        public List<PostDTO> Posts { get; set; }
        [Required]
        public PageInfoModel Pagination { get; set; }
    }
}
