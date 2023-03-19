using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Data;
using DAL.Model.Entities;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetUserAsync(string email, string password);

        Task<List<User>> GetAllUsersAsync();
    }
}
