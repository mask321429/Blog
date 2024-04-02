using Blog2023.Data.DTO;
using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.Data.Models
{
    public class CommunityModel
    {
        public Guid Id { get; set; }

        [Required]
        [MinLength(1)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public bool IsClosed { get; set; }
        [Required]
        public int SubscribersCount { get; set; }

        public DateTime? CreateTime { get; set; }

        public List<AdministratorDTO> Administrators { get; set; }

        public List<PostModel> Posts { get; set; }
    }


}
