
using FluentValidation;
using BLL.Model;
using DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace BLL.Validators
{
    public class RegisterModelValidator:AbstractValidator<RegisterModel>
    {
        private readonly LibDbContext _context;

        public RegisterModelValidator(LibDbContext context) 
        { 
            _context = context;

            RuleFor(x=>x.Email).NotNull().EmailAddress().Custom((email, context) =>
            {
                if (_context.Users.Any(x => x.Email == email))
                {
                    context.AddFailure("The email is used by another user");
                }
            });
            RuleFor(x => x.Username).NotNull().NotEmpty().Length(4, 20).Custom((username, context) =>
            {
                if (_context.Users.Any(x => x.Username == username))
                {
                    context.AddFailure("The username is used by another user");
                }
            });
            RuleFor(x=>x.Password).NotNull().NotEmpty().Length(6,20);
            RuleFor(x=>x.ConfirmPassword).NotNull().NotEmpty().Equal(x=>x.Password);
        }
    }
}
