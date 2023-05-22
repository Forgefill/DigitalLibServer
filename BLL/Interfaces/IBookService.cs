using BLL.Model.Book;

namespace BLL.Interfaces
{
    public interface IBookService
    {
        Task<OperationResult<List<BookModel>>> GetBookListAsync(BookFilters bookFilters, int page, int pageSize);

        Task<OperationResult<BookModel>> GetBookByTitleAsync(string title);

        Task<OperationResult<BookModel>> GetBookByIdAsync(int bookId);

        Task<OperationResult<UpdateBookModel>> UpdateBookAsync(int bookId, UpdateBookModel book);

        Task<OperationResult<BookModel>> DeleteBookAsync(int bookId);

        Task<OperationResult<CreateBookModel>> CreateBookAsync(CreateBookModel book);
    }
}
