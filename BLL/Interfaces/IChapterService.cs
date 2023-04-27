using BLL.Model.Chapter;

namespace BLL.Interfaces
{
    public interface IChapterService
    {
        public Task<OperationResult<ChapterModel>> GetChapterAsync(int chapterId);

        public Task<OperationResult<List<ChapterInfoModel>>> GetChapterListAsync(int bookId);

        public Task<OperationResult<ChapterInfoModel>> DeleteChapterAsync(int chapterId);

        public Task<OperationResult<ChapterModel>> UpdateChapterAsync(int chapterId, ChapterModel chapter);

        public Task<OperationResult<ChapterModel>> CreateChapterAsync(ChapterModel chapter);
    }
}
