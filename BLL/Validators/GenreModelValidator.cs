﻿using BLL.Model;
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
    public class GenreModelValidator : AbstractValidator<GenreModel>
    {
        private LibDbContext _context;

        public GenreModelValidator(LibDbContext context) {
            _context = context;

            RuleFor(x => x.Name).NotEmpty().NotNull().Custom((name, context) =>
            {
                if (_context.Genres.Any(x => x.Name == name))
                {
                    context.AddFailure("The genre already exists");
                }
            });
        }
    }
}
