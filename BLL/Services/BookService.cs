using AutoMapper;
using BLL.Interfaces;
using BLL.Model;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class BookService:IBookService
    {
        private LibDbContext context;
        private IMapper mapper;

        public BookService(LibDbContext libDbContext, IMapper _mapper) 
        {
            context = libDbContext;
            mapper = _mapper;
        }

        public async Task<OperationResult<List<BookInfoModel>>> GetAllBooksInfoAsync()
        {
            try
            {
                var books = await context.Books.Include(c=>c.Reviews).ToListAsync();
                return OperationResult<List<BookInfoModel>>.Success(mapper.Map<List<BookInfoModel>>(books));
            }
            catch (Exception ex)
            {
                return OperationResult<List<BookInfoModel>>.Failture(ex.Message);
            }
        }

        public async Task<OperationResult<BookInfoModel>> GetBookByTitleAsync(string title)
        {
            try
            {
                var book = await context.Books.Include(c=>c.Reviews).FirstAsync(x => x.Title == title);
                return OperationResult<BookInfoModel>.Success(mapper.Map<BookInfoModel>(book));
            }
            catch (Exception ex)
            {
                return OperationResult<BookInfoModel>.Failture(ex.Message);
            }
        }

        public async Task<OperationResult<BookModel>> GetBookByIdAsync(int bookId)
        {
            try
            {
                var book = await context.Books.FirstAsync(x => x.Id == bookId);
                return OperationResult<BookModel>.Success(mapper.Map<BookModel>(book));
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
                var image = await context.Images.FirstAsync(x => x.BookId == bookId);
                return OperationResult<ImageModel>.Success(mapper.Map<ImageModel>(image));
            }
            catch (Exception ex)
            {
                return OperationResult<ImageModel>.Failture(ex.Message);
            }
        }
    }
}
