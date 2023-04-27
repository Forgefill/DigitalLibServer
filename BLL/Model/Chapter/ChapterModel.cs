
namespace BLL.Model.Chapter
{
    public class ChapterModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int ChapterNumber { get; set; }

        public int BookId { get; set; }
    }
}
