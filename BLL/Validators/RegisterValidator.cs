using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using BLL.Model;

namespace BLL.Validators
{
    public class RegisterValidator:AbstractValidator<RegisterModel>
    {
        public RegisterValidator() 
        { 
            RuleFor(x=>x.Email).NotNull().EmailAddress();
            RuleFor(x => x.Username).NotNull().NotEmpty().Length(4, 20);
            RuleFor(x=>x.Password).NotNull().NotEmpty().Length(6,20);
            RuleFor(x=>x.ConfirmPassword).NotNull().NotEmpty().Equal(x=>x.Password);
        }
    }
}
