using DigitalLibServer.JWTAuth;
using DigitalLibServer.Model.DataContext;
using DigitalLibServer.Model.Entities;
using DigitalLibServer.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;

namespace DigitalLibServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private LibDbContext context;

        public LoginController(IConfiguration config, LibDbContext dbContext)
        {
            _config = config;
            context = dbContext;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel _User)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Login.ToLower() == _User.Login.ToLower() && x.Password == _User.Password);

            if(user == null)
            {
                return Unauthorized("Wrong login or password");
            }
            
            
            var claims = new List<Claim> { 
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", 
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            var jwt = new JwtSecurityToken(
                claims: claimsIdentity.Claims,
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(jwt), login = user.Login, role = user.Role });
        }

    }
}
