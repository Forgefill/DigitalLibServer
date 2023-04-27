using AutoMapper;
using BLL.Interfaces;
using BLL.Model;
using BLL.Validators;
using DAL.Data;
using DAL.Entities;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class UserService: IUserService
    {
        private LibDbContext _context;

        private ValidatorRepo _validators;

        private IMapper _mapper;

        public UserService(LibDbContext libDbContext, IMapper mapper, ValidatorRepo validators) 
        { 
            _context = libDbContext;
            _validators = validators;
            _mapper = mapper;
        }

        public async Task<OperationResult<UserModel>> GetUserAsync(string email, string password)
        {
            try
            {
                var user = await _context.Users.FirstAsync(x => x.Email == email && x.Password == password);
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
               var users = await _context.Users.ToListAsync();

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
                var user = await _context.Users.FirstAsync(x => x.Email == email);
                return OperationResult<UserModel>.Success(_mapper.Map<UserModel>(user));
            }
            catch (Exception ex)
            {
                return OperationResult<UserModel>.Failture(ex.Message);
            }
        }

        public async Task<OperationResult<UserModel>> RegisterUserAsync(RegisterModel registerModel)
        {
            ValidationResult validationResult = _validators.registerModelValidator.Validate(registerModel);

            if(!validationResult.IsValid) {
                return OperationResult<UserModel>.Failture(validationResult.Errors.Select(e=>e.ErrorMessage).ToArray());
            }

            try 
            { 
                User user = _mapper.Map<User>(registerModel);
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
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
