using BLL.Interfaces;
using BLL.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalLibServer.Controllers
{
    [Route("[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IUserService userService;
        private IAuthService authService;

        public AccountController(IUserService UserService, IAuthService AuthService)
        {
            userService = UserService;
            authService = AuthService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel User)
        {
            var result = await userService.GetUserAsync(User.Email, User.Password);

            if (!result.IsSuccess)
            {
                return Unauthorized(new { errors = new string[] { "Wrong login or password" } });
            }
              
            var tokenString = authService.GenerateToken(result.Entity);

            return Ok(new { data = new { token = tokenString, username = result.Entity.Username, email = result.Entity.Email } });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var result = await userService.RegisterUserAsync(model);

            if (!result.IsSuccess)
            {
                return BadRequest(new {errors = result.Errors });
            }

            var tokenResult = authService.GenerateToken(result.Entity);
            return Ok(new {data = new {token = tokenResult, username = result.Entity.Username, email = result.Entity.Email } });
        }

    }
}
