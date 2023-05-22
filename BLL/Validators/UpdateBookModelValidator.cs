using BLL.Model.Book;
using DAL.Data;
using FluentValidation;

namespace BLL.Validators
{


    public class UpdateBookModelValidator : AbstractValidator<UpdateBookModel>
    {
        private LibDbContext _context;

        public UpdateBookModelValidator(LibDbContext context)
        {
            _context = context;

            RuleFor(x => x.Id).NotEmpty().NotNull();
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
