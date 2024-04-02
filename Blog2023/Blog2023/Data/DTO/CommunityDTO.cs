using System;
using System.ComponentModel.DataAnnotations;

namespace Blog2023.Data.DTO
{
    public class CommunityDTO
    {
        public List<AdministratorDTO> Administrators { get; set; }
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
        
    }


}
