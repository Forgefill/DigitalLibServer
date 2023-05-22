using AutoMapper;
using BLL.Interfaces;
using BLL.Model.Book;
using BLL.Validators;
using DAL.Data;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;

namespace BLL.Services
{
    public class BookService:IBookService
    {
        private LibDbContext _context;
        private IMapper _mapper;
        private ValidatorRepo _validators;

        public BookService(LibDbContext libDbContext, IMapper mapper, ValidatorRepo validatorRepo) 
        {
            _context = libDbContext;
            _mapper = mapper;
            _validators = validatorRepo;
        }

        public async Task<OperationResult<List<BookModel>>> GetBookListAsync(BookFilters filters, int page, int pageSize)
        {
            var errors = new List<string>();

            if (page < 1)
            {
                errors.Add("Page must be greater than or equal to 1");
            }

            if (pageSize < 1 || pageSize > 100)
            {
                errors.Add("Page size must be between 1 and 100");
            }

            if (errors.Count > 0)
            {
                return OperationResult<List<BookModel>>.Failture(errors.ToArray());
            }

            try
            {
                var query = _context.Books.Include(c => c.Reviews).AsQueryable();

                if (filters != null)
                {
                    query = ApplyFiltering(query, filters);
                }

                query = ApplyOrdering(query, filters);

                var skipCount = (page - 1) * pageSize;
                var books = await query.Skip(skipCount).Take(pageSize).ToListAsync();

                return OperationResult<List<BookModel>>.Success(_mapper.Map<List<BookModel>>(books));
            }
            catch (Exception ex)
            {
                return OperationResult<List<BookModel>>.Failture(ex.Message);
            }
        }

        private IQueryable<Book> ApplyFiltering(IQueryable<Book> query, BookFilters filterModel)
        {

            if (filterModel.Genres != null && filterModel.Genres.Length > 0)
            {
                var genresList = filterModel.Genres.ToList();

                if (filterModel.GenreSelectLogic == GenreSelectLogic.Or)
                {
                    query = query.Where(b => _context.BookGenres
                        .Where(bg => bg.BookId == b.Id)
                        .Select(bg => bg.Genre.Name)
                        .Any(g => filterModel.Genres.Contains(g)));
                }
                else 
                {
                    query = query.Where(b => _context.BookGenres
                        .Where(bg => bg.BookId == b.Id)
                        .Select(bg => bg.Genre.Name)
                        .All(genre => filterModel.Genres.Contains(genre)));
                }
            }

            if (filterModel.Status != StatusFilter.All)
            {
                bool isCompleted = filterModel.Status == StatusFilter.Completed;
                query = query.Where(b => b.isCompleted == isCompleted);
            }

            if(filterModel.Rating != null)
            {
                query = filterModel.isLowerThanRating ? query.Where(x => x.AverageScore <= filterModel.Rating) 
                                                                    : query.Where(x => x.AverageScore >= filterModel.Rating);
            }

            return query;
        }

        private IQueryable<Book> ApplyOrdering(IQueryable<Book> query, BookFilters filterModel)
        {
            switch (filterModel?.OrderBy)
            {
                case OrderByFilter.ChapterCount:
                    query = query.OrderBy(b => b.Chapters.Count);
                    break;
                case OrderByFilter.Title:
                    query = query.OrderBy(b => b.Title);
                    break;
                case OrderByFilter.ReviewCount:
                    query = query.OrderByDescending(b => b.Reviews.Count);
                    break;
                case OrderByFilter.BookmarkCount:
                    query = query.OrderByDescending(b => b.Bookmarks);
                    break;
                case OrderByFilter.RatingScore:
                    query = query.OrderByDescending(b => b.AverageScore);
                    break;
            }

            return query;
        }

        public async Task<OperationResult<BookModel>> GetBookByTitleAsync(string title)
        {
            try
            {
                var book = await _context.Books.FirstAsync(x => x.Title == title);
                return OperationResult<BookModel>.Success(_mapper.Map<BookModel>(book));
            }
            catch (Exception ex)
            {
                return OperationResult<BookModel>.Failture(ex.Message);
            }
        }

        public async Task<OperationResult<BookModel>> GetBookByIdAsync(int bookId)
        {
            try
            {
                var book = await _context.Books.FirstAsync(x => x.Id == bookId);
                return OperationResult<BookModel>.Success(_mapper.Map<BookModel>(book));
            }
            catch (Exception ex)
            {
                return OperationResult<BookModel>.Failture(ex.Message);
            }
        }

        public async Task<OperationResult<BookModel>> DeleteBookAsync(int bookId)
        {
            if (!_context.Books.Any(x => x.Id == bookId))
                return OperationResult<BookModel>.Failture("The specified book ID was not found in the database");

            try
            {
                var book = await _context.Books.FirstAsync(x=>x.Id == bookId);
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
                return OperationResult<BookModel>.Success(_mapper.Map<BookModel>(book));
            }
            catch(Exception ex)
            {
                return OperationResult<BookModel>.Failture("Internal database error");
            }
        }


        public async Task<OperationResult<CreateBookModel>> CreateBookAsync(CreateBookModel bookModel)
        {
            var validationResult = _validators.createBookModelValidator.Validate(bookModel);

            if(!validationResult.IsValid)
                return OperationResult<CreateBookModel>.Failture(validationResult.Errors.Select(x => x.ErrorMessage).ToArray());

            try
            {
                var book = _mapper.Map<Book>(bookModel);
                await _context.Books.AddAsync(book);
                await _context.SaveChangesAsync();

                return OperationResult<CreateBookModel>.Success(bookModel);
            }
            catch (Exception ex)
            {
                return OperationResult<CreateBookModel>.Failture("Internal database error");
            }
        }


        public async Task<OperationResult<UpdateBookModel>> UpdateBookAsync(int bookId, UpdateBookModel bookModel)
        {
            var errors = new List<string>();
            var validationResult = _validators.updateBookModelValidator.Validate(bookModel);

            if (bookModel.Id != bookId)
            {
                errors.Add("The ID in the request URL does not match the ID in the model");
            }
            if (!_context.Books.Any(x => x.Id == bookId))
            {
                errors.Add("The specified book ID was not found in the database");
            }
            if (!validationResult.IsValid)
            {
                errors.AddRange(validationResult.Errors.Select(x => x.ErrorMessage));
            }
            if (errors.Count > 0)
                return OperationResult<UpdateBookModel>.Failture(errors.ToArray());

            try
            {
                var book = _mapper.Map<Book>(bookModel);
                _context.Books.Update(book);
                await _context.SaveChangesAsync();

                return OperationResult<UpdateBookModel>.Success(bookModel);
            }
            catch (Exception ex)
            {
                return OperationResult<UpdateBookModel>.Failture("Internal database error");
            }
        }
    }
}
