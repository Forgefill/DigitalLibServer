using BLL.Model.Review;
using DAL.Data;
using FluentValidation;

namespace BLL.Validators
{
    public class ReviewModelValidator:AbstractValidator<ReviewModel>
    {
        private LibDbContext _context;

        public ReviewModelValidator(LibDbContext context) 
        {
            _context = context; 
            RuleFor(x => x.UserId).NotNull().NotEmpty().Custom((id, context) =>
            {
                if (!_context.Users.Any(x => x.Id == id))
                {
                    context.AddFailure("Author id does not exist in database");
                }
            }); 
            RuleFor(x=>x.BookId).NotNull().NotEmpty().Custom((id, context) =>
            {
                if (!_context.Books.Any(x => x.Id == id))
                {
                    context.AddFailure("Book id does not exist in database");
                }
            });
            RuleFor(x => x.Content).NotNull().NotEmpty().MaximumLength(5000);
            RuleFor(x => x.Score).NotNull().GreaterThanOrEqualTo(0).LessThanOrEqualTo(5);
        }
    }
}
