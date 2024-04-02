using Blog.Data.DTO;
using Blog.Data.Models;
using Blog2023.Data.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Services.Interfaces
{
    public interface ICommentService
    {
        Task<IActionResult> AddComment(CreateCommentDTO postsListQuery, Guid id, Guid userId);

        Task<List<GetCommentDTO>> GetTree(Guid guid);

        Task<IActionResult> EditComment(EditContentDTO Content, Guid id, Guid idUser);

        Task<IActionResult> DeleteComment(Guid idComment, Guid idUser);

    }
}
