using System.ComponentModel.DataAnnotations;

namespace Blog2023.Data.Models
{
    public class CommunityUserModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CommunityId { get; set; }
        public Guid UserId { get; set; }
        public string Role { get; set; }
    }
}
