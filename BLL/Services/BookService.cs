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

        public async Task<OperationResult<List<BookModel>>> GetAllBooksAsync()
        {
            try
            {
                var books = await context.Books.ToListAsync();
                return OperationResult<List<BookModel>>.Success(mapper.Map<List<BookModel>>(books));
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
                var book = await context.Books.FirstAsync(x => x.Title == title);
                return OperationResult<BookModel>.Success(mapper.Map<BookModel>(book));
            }
            catch (Exception ex)
            {
                return OperationResult<BookModel>.Failture(ex.Message);
            }
        }
    }
}
