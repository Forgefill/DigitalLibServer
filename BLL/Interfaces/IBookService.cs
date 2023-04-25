using BLL.Model;

namespace BLL.Interfaces
{
    public interface IBookService
    {
        Task<OperationResult<List<BookInfoModel>>> GetAllBooksInfoAsync();

        Task<OperationResult<BookInfoModel>> GetBookByTitleAsync(string title);

        Task<OperationResult<BookModel>> GetBookByIdAsync(int bookId);

        Task<OperationResult<ImageModel>> GetImageAsync(int bookId);

    }
}
