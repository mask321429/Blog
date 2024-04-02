using Blog.Data;
using Blog.Data.Models;
using Blog.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Blog.Services
{

    public class TokenService : ITokenService
    {
        private readonly ApplicationDbContext _context;

        public User? GetUserWithToken(string jwt)
        {
            return null;
        }

        public bool IsValidToken(string jwt)
        {
            var token = _context.Tokens.FirstOrDefault(x => x.InvalidToken == jwt);
            return token == null;
        }

        public void ClearInvalidTokens()
        {
            var invalidTokens = _context.Tokens.Where(token => IsInvalidToken(token.InvalidToken)).ToList();
            _context.Tokens.RemoveRange(invalidTokens);
            _context.SaveChanges();
        }

        [Authorize]
        private bool IsInvalidToken(string? jwt)
        {
            return jwt == null;
        }
    }
}
