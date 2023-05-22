
namespace BLL.Model.Book
{
    public class UpdateBookModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public bool isCompleted { get; set; }
    }
}
