using Blog.Services;
using Blog.Services.Interfaces;
using Blog2023.Data.DTO;
using Blog2023.Migrations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog2023.Controllers
{
    [ApiController]
    [Route("api/tag")]

    public class TagController : Controller
    {
        private readonly ITagService _tagService;
        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetUserProfile()
        {
            var values = await _tagService.Tag(); 

            if (values == null)
            {
                return NotFound();
            }

            return Ok(values);


        }
    }
}
