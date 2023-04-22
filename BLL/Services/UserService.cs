using AutoMapper;
using BLL.Interfaces;
using BLL.Model;
using BLL.Validators;
using DAL.Data;
using DAL.Model.Entities;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class UserService: IUserService
    {
        private LibDbContext context;

        private ValidationRepo _validators;

        private IMapper _mapper;

        public UserService(LibDbContext libDbContext, IMapper mapper) 
        { 
            context = libDbContext;
            _validators = new ValidationRepo();
            _mapper = mapper;
        }

        public async Task<OperationResult<UserModel>> GetUserAsync(string email, string password)
        {
            try
            {
                var user = await context.Users.FirstAsync(x => x.Email == email && x.Password == password);
                var userModel = _mapper.Map<UserModel>(user);
                
                return OperationResult<UserModel>.Success(userModel);
            }
            catch(Exception ex)
            {
                return OperationResult<UserModel>.Failture(ex.Message);
            }
        }

        public async Task<OperationResult<List<UserModel>>> GetAllUsersAsync()
        {
            try
            {
               var users = await context.Users.ToListAsync();

               return OperationResult<List<UserModel>>.Success(_mapper.Map<List<UserModel>>(users));
            }
            catch (Exception ex)
            {
                return OperationResult<List<UserModel>>.Failture(ex.Message);
            }
        }

        public async Task<OperationResult<UserModel>> GetUserByEmailAsync(string email)
        {
            try
            {
                var user = await context.Users.FirstAsync(x => x.Email == email);
                return OperationResult<UserModel>.Success(_mapper.Map<UserModel>(user));
            }
            catch (Exception ex)
            {
                return OperationResult<UserModel>.Failture(ex.Message);
            }
        }

        public async Task<OperationResult<UserModel>> RegisterUserAsync(RegisterModel registerModel)
        {
            ValidationResult vr = _validators.registerValidator.Validate(registerModel);

            if(!vr.IsValid) {
                return OperationResult<UserModel>.Failture(vr.Errors.Select(e=>e.ErrorMessage).ToArray());
            }

            var userExist  = context.Users.FirstOrDefault(x => x.Email == registerModel.Email) != null;
            if(userExist) {
                return OperationResult<UserModel>.Failture("The email is used by another user");
            }

            User user = _mapper.Map<User>(registerModel);
            try
            {
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
                var userModel = _mapper.Map<UserModel>(registerModel);
                return OperationResult<UserModel>.Success(userModel);
            }
            catch (Exception ex)
            {
                return OperationResult<UserModel>.Failture(ex.Message);
            }
        }
    }
}
