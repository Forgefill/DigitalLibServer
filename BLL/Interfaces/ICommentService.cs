using BLL.Model.Comment;

namespace BLL.Interfaces
{
    public interface ICommentService
    {
        public Task<OperationResult<List<CommentInfoModel>>> GetCommentListAsync(int chapterId);

        public Task<OperationResult<CommentInfoModel>> DeleteCommentAsync(int commentId);

        public Task<OperationResult<CommentModel>> UpdateCommentAsync(int commentId, CommentModel comment);

        public Task<OperationResult<CommentModel>> CreateCommentAsync(CommentModel comment);
    }
}
