using Blog.Data.Models;
using Blog.Services.Interfaces;
using Blog2023.Data.DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Blog.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;

        public UserController(IAuthService usersService)
        {
            _authService = usersService;
        }


        [HttpPost]
        [Route("register")]
        public async Task<TokenDTO> Register([FromBody] UserRegisterDTO userRegister)
        {
            return await _authService.Register(userRegister);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO forSuccessfulLogin)
        {
            try
            {
                if (forSuccessfulLogin.Email == null)
                {
                    return StatusCode(401, "Email cannot be null.");
                }

                var token = await _authService.Login(forSuccessfulLogin);

                if (token != null)
                {
                    return Ok(new { Token = token });
                }
                else
                {
                    return StatusCode(401, "Invalid credentials.");
                }
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(401, "User does not exist.");
            }
            catch (Exception ex)
            {
                // Выводим информацию об исключении в консоль
                Console.WriteLine($"An error occurred while logging in: {ex}");

                // Возвращаем сообщение об ошибке
                return StatusCode(500, "An error occurred while logging in. Please check the console for more details.");
            }
        }





        [HttpPost("logout")]
        public async Task Logout()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            if (token == null)
            {
                throw new InvalidOperationException("Access token not found in the current context.");
            }
            else
            {
                await _authService.Logout(token);

            }
        }

        [HttpGet]
        [Authorize]
        [Route("profile")]
        public async Task<UserDTO> GetUserProfile()
        {
            return await _authService.GetInfoProfile(
                Guid.Parse(User.Identity.Name));
        }

        [HttpPut]
        [Authorize]
        [Route("profile")]
        public async Task EditUserProfile([FromBody] userEditModel userEditModel)
        {
            await _authService.EditProfile(
                Guid.Parse(User.Identity.Name), userEditModel);
        }
    }
}

