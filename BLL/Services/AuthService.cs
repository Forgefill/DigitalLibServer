using BLL.Interfaces;
using BLL.JWTAuth;
using DAL.Model.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BLL.Services
{
    public class AuthService:IAuthService
    {

        public AuthService() { }

        public object GenerateToken(User user)
        {
            var claims = new List<Claim> {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            var jwt = new JwtSecurityToken(
                claims: claimsIdentity.Claims,
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new { token = new JwtSecurityTokenHandler().WriteToken(jwt), email = user.Email, role = user.Role, username = user.Username };
        }
    }
}
