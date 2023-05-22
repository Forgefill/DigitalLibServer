using BLL.Model;

namespace BLL.Interfaces
{
    public interface IImageService
    {
        Task<OperationResult<ImageModel>> GetImageAsync(int bookId);

        Task<OperationResult<ImageModel>> CreateImageAsync(ImageModel image);

        Task<OperationResult<ImageModel>> UpdateImageAsync(int bookId, ImageModel image);

        Task<OperationResult<ImageModel>> DeleteImageAsync(int bookId);
    }
}
