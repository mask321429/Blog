using Blog.Data.Models;
using Blog2023.Data.DTO;

namespace Blog.Services.Interfaces
{
    public interface IAuthService
    {
        Task<TokenDTO> Register(UserRegisterDTO userRegisterDTO);
        Task<TokenDTO> Login(UserLoginDTO ForSuccessfulLogin);
        Task<UserDTO> GetInfoProfile(Guid UserId);
        Task EditProfile(Guid UserId, userEditModel user);
        Task Logout(string token);

    }
}
