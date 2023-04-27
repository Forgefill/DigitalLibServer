using BLL.Model.Book;
using DAL.Data;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validators
{
    public class CreateBookModelValidator : AbstractValidator<CreateBookModel>
    {
        private LibDbContext _context;

        public CreateBookModelValidator(LibDbContext context)
        {
            _context = context;

            RuleFor(x => x.Title).NotEmpty().NotNull().Custom((title, context) =>
            {
                if (_context.Books.Any(x => x.Title == title))
                {
                    context.AddFailure("The title already exists");
                }
            });

            RuleFor(x => x.AuthorId).NotNull().Custom((authorId, context) =>
            {
                if (!_context.Users.Any(x => x.Id == authorId))
                {
                    context.AddFailure("The author does not exist");
                }
            });
        }
    }
}
