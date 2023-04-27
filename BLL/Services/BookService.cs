using AutoMapper;
using BLL.Interfaces;
using BLL.Model;
using BLL.Model.Book;
using BLL.Validators;
using DAL.Data;
using DAL.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

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

        public async Task<OperationResult<List<BookModel>>> GetBookListAsync()
        {
            try
            {
                var books = await _context.Books.Include(c=>c.Reviews).ToListAsync();
                return OperationResult<List<BookModel>>.Success(_mapper.Map<List<BookModel>>(books));
            }
            catch (Exception ex)
            {
                return OperationResult<List<BookModel>>.Failture(ex.Message);
            }
        }

        public async Task<OperationResult<BookModel>> GetBookByTitleAsync(string title)
        {
            try
            {
                var book = await _context.Books.Include(c=>c.Reviews).FirstAsync(x => x.Title == title);
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

        public async Task<OperationResult<ImageModel>> GetImageAsync(int bookId)
        {
            try
            {
                var image = await _context.Images.FirstAsync(x => x.BookId == bookId);
                return OperationResult<ImageModel>.Success(_mapper.Map<ImageModel>(image));
            }
            catch (Exception ex)
            {
                return OperationResult<ImageModel>.Failture(ex.Message);
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
