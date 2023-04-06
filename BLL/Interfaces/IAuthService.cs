using BLL.Model;

namespace BLL.Interfaces
{
    public interface IAuthService
    {

        public object GenerateToken(UserModel user);
    }
}
