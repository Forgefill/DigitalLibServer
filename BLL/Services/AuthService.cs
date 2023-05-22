using BLL.Interfaces;
using BLL.JWTAuth;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BLL.Model;

namespace BLL.Services
{
    public class AuthService:IAuthService
    {

        public AuthService() { }

        public string GenerateToken(UserModel user)
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

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public UserModel DecodeToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            // Remove "Bearer " prefix from token string
            token = token.Substring("Bearer ".Length).Trim();

            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false
                };

                SecurityToken validatedToken;
                var claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);

                var userModel = new UserModel
                {
                    
                    Email = claimsPrincipal.FindFirst(ClaimTypes.Email)?.Value,
                    Username = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value,
                    Role = claimsPrincipal.FindFirst(ClaimTypes.Role)?.Value
                };

                return userModel;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
