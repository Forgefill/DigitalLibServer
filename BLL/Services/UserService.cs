using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.Data;
using DAL.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class UserService:IUserService
    {
        private LibDbContext context;

        public UserService(LibDbContext libDbContext) 
        { 
            context = libDbContext;
        }

        public async Task<User?> GetUserAsync(string email, string password)
        {
            return await context.Users.FirstOrDefaultAsync(x=>x.Email == email && x.Password == password);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await context.Users.ToListAsync();
        }
    }
}
