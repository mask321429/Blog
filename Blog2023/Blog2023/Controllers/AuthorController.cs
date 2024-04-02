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
    [Route("api/author")]

    public class AuthorController : Controller
    {
        private readonly IAuthorService _AuthorService;
        public AuthorController(IAuthorService authorService)
        {
            _AuthorService = authorService;
        }
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetAuthor()
        {
            var values = await _AuthorService.GetAuthor();

            if (values == null)
            {
                return NotFound();
            }

            return Ok(values);


        }

        [HttpPost]
        [Route("create/author")]
        public async Task<IActionResult> CreateNewAuthor(CreateNewAuthor createNewAuthor)
        {
            var result = _AuthorService.CreateNewAuthor(createNewAuthor);
            return 
        }
    }
}
