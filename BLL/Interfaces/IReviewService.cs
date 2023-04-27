using BLL.Model.Review;

namespace BLL.Interfaces
{
    public interface IReviewService
    {
        public Task<OperationResult<List<ReviewInfoModel>>> GetReviewListAsync(int bookId);

        public Task<OperationResult<ReviewInfoModel>> DeleteReviewAsync(int reviewId);

        public Task<OperationResult<ReviewModel>> UpdateReviewAsync(int reviewId, ReviewModel review);

        public Task<OperationResult<ReviewModel>> CreateReviewAsync(ReviewModel review);
    }
}
