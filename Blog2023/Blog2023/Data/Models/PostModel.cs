using Blog2023.Data.DTO;
using Blog2023.Data.Models;
using System.Diagnostics;

namespace Blog.Data.Models
{
    public class PostModel
    {
        public Guid Id { get; set; }
        public DateTime CreateTime { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ReadingTime { get; set; }
        public string? Image { get; set; }
        public Guid? AuthorId { get; set; }
        public string Author { get; set; }
        public Guid CommunityId { get; set; }
        public string? CommunityName { get; set; }
        public Guid AddressId { get; set; }
        public int Likes { get; set; }
        public bool HasLike { get; set; }
        public int CommentsCount { get; set; }
        public List<CommentDTO> Comments { get; set; }
        public List<TagModel> Tags { get; set; }
    }

    }

