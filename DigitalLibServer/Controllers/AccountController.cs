using AutoMapper;
using BLL.Interfaces;
using BLL.JWTAuth;
using BLL.Model;
using BLL.Services;
using DAL.Model.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DigitalLibServer.Controllers
{
    [Route("[controller]")]
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
                return Unauthorized("Wrong login or password");
            }
            else
            {
                var tokenObj = authService.GenerateToken(result.Entity);
                return Ok(tokenObj);
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var result = await userService.RegisterUserAsync(model);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            var tokenObj = authService.GenerateToken(result.Entity);
            return Ok(tokenObj);
        }

    }
}
