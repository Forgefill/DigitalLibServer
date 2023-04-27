using BLL.Model;

namespace BLL.Interfaces
{
    public interface IGenreService
    {

        public Task<OperationResult<GenreModel>> GetGenreByIdAsync(int genreId);

        public Task<OperationResult<List<GenreModel>>> GetGenreListAsync();

        public Task<OperationResult<GenreModel>> DeleteGenreAsync(int genreId);

        public Task<OperationResult<GenreModel>> UpdateGenreAsync(int genreId, GenreModel genre);

        public Task<OperationResult<GenreModel>> CreateGenreAsync(GenreModel genre);
    }
}
