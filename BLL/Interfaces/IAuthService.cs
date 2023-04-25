using BLL.Model;

namespace BLL.Interfaces
{
    public interface IAuthService
    {

        public string GenerateToken(UserModel user);
    }
}
