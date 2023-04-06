using BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IBookService
    {
        Task<OperationResult<List<BookModel>>> GetAllBooksAsync();

        Task<OperationResult<BookModel>> GetBookByTitleAsync(string title);

    }
}
