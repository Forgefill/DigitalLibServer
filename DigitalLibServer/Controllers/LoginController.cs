using BLL.Interfaces;
using BLL.JWTAuth;
using BLL.Model;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DigitalLibServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private UserService userService;
        private AuthService authService;

        public LoginController(UserService UserService, AuthService AuthService)
        {
            userService = UserService;
            authService = AuthService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel _User)
        {
            var user = await userService.GetUserAsync(_User.Email, _User.Password);

            if (user == null)
            {
                return Unauthorized("Wrong login or password");
            }
            else
            {
                var tokenObj = authService.GenerateToken(user);
                return Ok(tokenObj);
            }
        }

        //[AllowAnonymous]
        //[HttpPost]
        //public async Task<IActionResult> Register()
        //{

        //}

    }
}
