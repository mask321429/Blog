using Blog.Data.DTO;
using Blog.Data.Models;
using Blog2023.Data.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Services.Interfaces
{
    public interface ICommunityService
    {
        Task<List<CommunityDTO>> GetCommunity();

        Task<List<MyCommunityDTO>> GetMyCommunity(Guid userId);

        Task<CommunityDTO> GetCommunityId(Guid communityId);

        Task<object> GetPostList(GetPostListQuery postsListQuery);

        Task<IActionResult> PostPostList(PostWithTagsDTO postsListQuery, Guid id, Guid userId);


        Task<bool> SubscribeOnCommunity(Guid idCummunity,Guid idUser);
        Task UnsubscribeFromCommunity(Guid idCummunity, Guid idUser);

        Task<string> CommunityRole(Guid idCummunity, Guid idUser);
    }
}
