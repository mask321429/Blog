using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.Data.Models
{
    public class MyCommunityModel
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid CommunityId { get; set; }

        public string Role { get; set; }
    }
}
