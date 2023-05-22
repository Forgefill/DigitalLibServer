
namespace BLL.Model.Book
{
    public class BookModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public int Views { get; set; }

        public int Bookmarks { get; set; }

        public bool isCompleted { get; set; }

        public double AverageScore { get; set; }

        public int AuthorId { get; set; }

        public int? ImageId { get; set; }
    }
}
