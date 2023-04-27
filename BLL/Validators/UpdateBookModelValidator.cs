using BLL.Model.Book;
using DAL.Data;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BLL.Validators
{


    public class UpdateBookModelValidator : AbstractValidator<UpdateBookModel>
    {
        private LibDbContext _context;

        public UpdateBookModelValidator(LibDbContext context)
        {
            _context = context;

            RuleFor(x => x.Id).NotEmpty().NotNull().Custom((id, context) =>
            {
                if (!_context.Books.Any(x => x.Id == id))
                {
                    context.AddFailure("The specified book ID was not found in the database");
                }
            });
            RuleFor(x => x.Title).NotEmpty().NotNull().Custom((title, context) =>
            {
                if (_context.Books.Any(x => x.Title == title))
                {
                    context.AddFailure("The title already exists");
                }
            });
        }
    }

    
}
