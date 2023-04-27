using BLL.Model.Comment;
using DAL.Data;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validators
{
    public class CommentModelValidator:AbstractValidator<CommentModel>
    {
        private LibDbContext _context;

        public CommentModelValidator(LibDbContext context)
        {
            _context = context;
            RuleFor(x => x.UserId).NotNull().NotEmpty().Custom((id, context) =>
            {
                if (!_context.Users.Any(x => x.Id == id))
                {
                    context.AddFailure("User id does not exist in database");
                }
            });
            RuleFor(x => x.ChapterId).NotNull().NotEmpty().Custom((id, context) =>
            {
                if (!_context.Chapters.Any(x => x.Id == id))
                {
                    context.AddFailure("Chapter id does not exist in database");
                }
            });
            RuleFor(x => x.Content).NotNull().NotEmpty().MaximumLength(1000);
        }
    }
}
