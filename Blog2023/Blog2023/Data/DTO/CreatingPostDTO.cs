using Blog2023.Data.DTO;
using System.ComponentModel.DataAnnotations;

namespace Blog.Data.DTO
{
    public class PostWithTagsDTO
    {
       
            [Required(ErrorMessage = "Title is required")]
            public string Title { get; set; }

            [Required(ErrorMessage = "Description is required")]
            public string Description { get; set; }

            [Required(ErrorMessage = "Reading time is required")]
            public int ReadingTime { get; set; }

            public string Image { get; set; }
            public Guid AddressId { get; set; }
            public List<TagDTOForNewPost> Tags { get; set; }
        
    }
}
