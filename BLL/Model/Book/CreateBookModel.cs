
namespace BLL.Model.Book
{
    public class CreateBookModel
    {
        public string Title { get; set; }

        public string? Description { get; set; }

        public int AuthorId { get; set; }
    }
}
