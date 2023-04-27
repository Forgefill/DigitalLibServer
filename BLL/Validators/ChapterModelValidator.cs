using BLL.Model.Chapter;
using DAL.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validators
{
    public class ChapterModelValidator:AbstractValidator<ChapterModel>
    {

        private LibDbContext _context {  get; set; }

        public ChapterModelValidator(LibDbContext context) 
        {
            _context = context;

            RuleFor(x => x.Title).MaximumLength(100).NotEmpty().NotNull();
            RuleFor(x => x.Content).NotEmpty().NotNull();
            RuleFor(x => x.ChapterNumber).NotNull().NotEmpty();
            RuleFor(x => x.BookId).NotNull().Custom((id, context) =>
            {
                if (!_context.Books.Any(x => x.Id == id))
                {
                    context.AddFailure("Book id does not exist in database");
                }
            }); 
        }
    }
}
