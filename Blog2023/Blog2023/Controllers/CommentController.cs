using Azure;
using Blog.Data.DTO;
using Blog.Data.Models;
using Blog.Services;
using Blog.Services.Interfaces;
using Blog2023.Data.DTO;
using Blog2023.Migrations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Security.Claims;

namespace Blog.Controllers
{
    [ApiController]
    [Route("api/")]

    public class CommentController : ControllerBase
    {
        private readonly ICommentService _CommentService;
        public CommentController(ICommentService commentService)
        {
            _CommentService = commentService;
        }

        [HttpGet]
        [Route("comment/{id}/tree")]
        public async Task<ActionResult<List<GetCommentDTO>>> GetTree(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequest("Invalid ID");
                }

                var comments = await _CommentService.GetTree(id);

                if (comments.Count == 0)
                {
                    return NotFound("No comments found for this ID");
                }

                return Ok(comments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка сервера: {ex.Message}");
            }
        }


        [HttpPost]
        [Route("post/{id}/comment")]
        [Authorize]
        public async Task<IActionResult> AddComment([FromBody] CreateCommentDTO createCommentDto, Guid id)
        {
            string userIdString = User.Identity.Name;

            if (Guid.TryParse(userIdString, out Guid userId))
            {
                try
                {
                    await _CommentService.AddComment(createCommentDto, id, userId);
                    return Ok("Comment added successfully.");
                }
                catch (Exception ex)
                {
                    string innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : "No inner exception.";
                    return StatusCode(400, $"An error occurred: {ex.Message}. Inner exception: {innerExceptionMessage}");
                }
            }
            else
            {
                return BadRequest("Invalid user ID format.");
            }
        }
        [HttpPut]
        [Route("comment/{id}")]
        [Authorize]
        public async Task<IActionResult> EditComment([FromBody] EditContentDTO Content, Guid id)
        {
            string userIdString = User.Identity.Name;

            if (Guid.TryParse(userIdString, out Guid userId))
            {
                try
                {
                    await _CommentService.EditComment(Content, id, userId);
                    return Ok("Comment added successfully.");
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
        [HttpDelete]
        [Route("comment/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            var check = await _CommentService.DeleteComment(id, Guid.Parse(User.Identity.Name));
            if (check != null)
            {
                return Ok("Ok");
            }
            else
            {
                return BadRequest("Error");
            }
        }




    }
}
