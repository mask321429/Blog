using Azure;
using Blog.Data.DTO;
using Blog.Data.Models;
using Blog.Services;
using Blog.Services.Interfaces;
using Blog2023.Data.DTO;
using Blog2023.Data.enums;
using Blog2023.Migrations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Blog.Controllers
{
    [ApiController]
    [Route("api/post")]

    public class PostController : ControllerBase
    {
        private readonly IPostService _PostService;
        public PostController(IPostService communityService)
        {
            _PostService = communityService;
        }

        [HttpGet]
        public async Task<ActionResult<PostWithPagination>> GetPostsAsync([FromQuery] Guid[]? tags, string? author = null, int? min = null, int? max = null, PostSorting? sorting = null, int page = 1, int size = 5)
        {

            try
            {
                string userIdString = User.Identity.Name;

                if (!Guid.TryParse(userIdString, out Guid userId))
                {
                    throw new ArgumentException("Invalid user ID format.");
                }

                var postInfo = await _PostService.GetPost(tags, author, min, max, sorting, page, size, userId);

                if (postInfo != null)
                {
                    return postInfo;
                }
                else return null;
            }
            catch (ArgumentException ex)
            {
                
                throw new Exception($"An error occurred: {ex.Message}");
            }
            catch (KeyNotFoundException ex)
            {
               
                throw new Exception($"An error occurred: {ex.Message}");
            }
            catch (Exception ex)
            {
               
                throw new Exception($"An error occurred: {ex.Message}");
            }
        }




        [HttpPost("")]
        [Authorize]
        public async Task<IActionResult> PostPostList([FromBody] PostWithTagsDTO postsListQuery)
        {
            string userIdString = User.Identity.Name;


            Console.WriteLine($"User.Identity.Name: {userIdString}");

            if (Guid.TryParse(userIdString, out Guid userId))
            {
                try
                {
                    await _PostService.PostList(postsListQuery, userId);
                    return Ok("Post created successfully.");
                }
                catch (Exception ex)
                {
                   
                    string innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : "No inner exception.";
                    return StatusCode(500, $"An error occurred: {ex.Message}. Inner exception: {innerExceptionMessage}");
                }
            }
            else
            {
                return BadRequest("Invalid user ID format.");
            }
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<PostDTO> GetInfoPost(Guid id)
        {
            try
            {
                string userIdString = User.Identity.Name;

                if (!Guid.TryParse(userIdString, out Guid userId))
                {
                    throw new ArgumentException("Invalid user ID format.");
                }

                var postInfo = await _PostService.GetInfoPost(id, userId);

                if (postInfo == null)
                {
                    throw new KeyNotFoundException($"Post with ID '{id}' not found.");
                }

                return postInfo;
            }
            catch (ArgumentException ex)
            {
               
                throw new Exception($"An error occurred: {ex.Message}");
            }
            catch (KeyNotFoundException ex)
            {
                
                throw new Exception($"An error occurred: {ex.Message}");
            }
            catch (Exception ex)
            {

                throw new Exception($"An error occurred: {ex.Message}");
            }
        }







        [HttpPost("{id}/like")]
        [Authorize]
        public async Task<IActionResult> AddLike(Guid id)
        {
            var result = await _PostService.LikeAdd(id, Guid.Parse(User.Identity.Name));
            if (result)
            {
                return Ok("Пост успешно лайкнут");
            }
            else
            {
                return BadRequest("Лайк уже был поставлен ранее");
            }
        }


        [HttpDelete("{id}/delete")]
        [Authorize]
        public async Task<IActionResult> DeleteLike(Guid IdPost, Guid IdUser)
        {
            var result = await _PostService.DeleteLike(IdPost, IdUser);
            if (result)
            {
                return Ok("Лайк успешно удален");
            }
            else
            {
                return BadRequest("Лайк не найден");
            }
        }

        /*[HttpDelete("{id}/like")]
        [Authorize]
        public async Task UnsubscribeFromCommunity(Guid id)
        {
            await _PostService.UnsubscribeFromCommunity(id, Guid.Parse(User.Identity.Name));

        }*/
    }

}
