using Blog.Data.DTO;
using Blog.Data.Models;
using Blog.Services;
using Blog.Services.Interfaces;
using Blog2023.Data.DTO;
using Blog2023.Migrations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.Controllers
{
    [ApiController]
    [Route("api/community")]

    public class CommunityController : ControllerBase
    {
        private readonly ICommunityService _CommunityService;
        public CommunityController(ICommunityService communityService)
        {
            _CommunityService = communityService;
        }
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetCommunity()
        {
            try
            {
                var values = await _CommunityService.GetCommunity();
                return Ok(values);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet]
        [Authorize]
        [Route("my")]
        public async Task<IActionResult> GetMyCommunity()
        {
            try
            {
                if (!Guid.TryParse(User.Identity.Name, out Guid userId))
                {
                    return BadRequest("Invalid user ID format");
                }

                var myCommunity = await _CommunityService.GetMyCommunity(userId);
                return Ok(myCommunity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCommunityId(Guid id)
        {
            try
            {
                var community = await _CommunityService.GetCommunityId(id);
                if (community == null)
                {
                    return NotFound("Community not found");
                }
                return Ok(community);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("{id}/post")]
        public async Task<IActionResult> GetPostList([FromQuery] GetPostListQuery postsListQuery)
        {
            try
            {
                var communityPostList = await _CommunityService.GetPostList(postsListQuery);
                return Ok(communityPostList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("{id}/post")]
        [Authorize]
        public async Task<IActionResult> PostPostList([FromBody] PostWithTagsDTO postsListQuery, Guid id)
        {
            try
            {
                string userIdString = User.Identity.Name;

                if (Guid.TryParse(userIdString, out Guid userId))
                {
                    if (string.IsNullOrWhiteSpace(postsListQuery.Title) || string.IsNullOrWhiteSpace(postsListQuery.Description) || postsListQuery.ReadingTime <= 0)
                    {
                        return BadRequest("Title, Description, and ReadingTime are required fields.");
                    }

                    await _CommunityService.PostPostList(postsListQuery, id, userId);
                    return Ok("Post created successfully.");
                }
                else
                {
                    return BadRequest("Invalid user ID format.");
                }
            }
            catch (Exception ex)
            {
                string innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : "No inner exception.";
                return StatusCode(500, $"An error occurred: {ex.Message}. Inner exception: {innerExceptionMessage}");
            }
        }


        [HttpGet("{id}/role")]
        [Authorize]
        public async Task<IActionResult> CommunityRole(Guid id)
        {
            try
            {
                var result = await _CommunityService.CommunityRole(id, Guid.Parse(User.Identity.Name));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("{id}/subscribe")]
        [Authorize]
        public async Task<IActionResult> SubscribeOnCommunity(Guid id)
        {
            try
            {
                var isSubscribed = await _CommunityService.SubscribeOnCommunity(id, Guid.Parse(User.Identity.Name));

                if (isSubscribed)
                {
                    return Ok("Subscribed successfully.");
                }
                else
                {
                    return BadRequest("Already subscribed to this community.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("{id}/unsubscribe")]
        [Authorize]
        public async Task<IActionResult> UnsubscribeFromCommunity(Guid id)
        {
            try
            {
                await _CommunityService.UnsubscribeFromCommunity(id, Guid.Parse(User.Identity.Name));
                return Ok("Unsubscribed successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

    }
}
