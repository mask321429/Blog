using System;
using System.ComponentModel.DataAnnotations;

namespace Blog2023.Data.DTO
{
    public class MyCommunityDTO
    {
        public Guid UserId { get; set; }

        public Guid CommunityId { get; set; }
        
        public string Role { get; set; }
    }


}
