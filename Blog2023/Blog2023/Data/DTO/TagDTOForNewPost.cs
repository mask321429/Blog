using System;
using System.ComponentModel.DataAnnotations;

namespace Blog2023.Data.DTO
{
    public class TagDTOForNewPost
    {
        [Key]
        public List<Guid> Id { get; set; }

    }
}
