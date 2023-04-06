using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BLL.JWTAuth
{
    public class AuthOptions
    {
        public static string Key { get; } = "UhVWKa0vmBjUcfAwHWar";

        public static string? ValidIssuer { get; } = null;

        public static string? ValidAudience { get; } = null;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }

    }
}
