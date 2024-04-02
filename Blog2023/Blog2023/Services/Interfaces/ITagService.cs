using Blog.Data.Models;
using Blog2023.Data.DTO;

namespace Blog.Services.Interfaces
{
    public interface ITagService
    {
        Task<List<TagDTO>> Tag();
    }
}
