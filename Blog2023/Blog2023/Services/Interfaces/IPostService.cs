using Blog.Data.DTO;
using Blog.Data.Models;
using Blog2023.Data.DTO;
using Blog2023.Data.enums;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Services.Interfaces
{
    public interface IPostService
    {
        Task<PostWithPagination> GetPost(Guid[]? tags, string author, int? min, int? max, PostSorting? sorting, int page, int size, Guid userId);
        Task<IActionResult> PostList(PostWithTagsDTO postsListQuery, Guid userId);
        Task<PostDTO> GetInfoPost(Guid idPost, Guid idUser);
        Task<bool> LikeAdd(Guid idPost, Guid idUser);
        Task<bool> DeleteLike(Guid idPost, Guid idUser);


    }
}
