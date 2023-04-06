using BLL.Model;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        Task<OperationResult<UserModel>> GetUserAsync(string email, string password);

        Task<OperationResult<List<UserModel>>> GetAllUsersAsync();

        Task<OperationResult<UserModel>> GetUserByEmailAsync(string email);

        Task<OperationResult<UserModel>> RegisterUserAsync(RegisterModel user);

    }
}
