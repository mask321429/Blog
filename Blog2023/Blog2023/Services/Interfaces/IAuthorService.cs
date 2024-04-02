using Blog.Data.Models;
using Blog2023.Data.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<List<AuthorDTO>> GetAuthor();
        Task<IActionResult> CreateNewAuthor(CreateNewAuthor createNewAuthor);
    }
}
