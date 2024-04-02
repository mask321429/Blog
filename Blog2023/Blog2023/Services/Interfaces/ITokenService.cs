using Blog.Data.Models;

namespace Blog.Services.Interfaces
{
    public interface ITokenService
    {
        User? GetUserWithToken(string jwt);
        bool IsValidToken(string jwt);
        void ClearInvalidTokens();
    }
}
