
namespace BLL.Model.Review
{
    public class ReviewModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int BookId { get; set; }

        public string Content { get; set; }

        public int Score { get; set; }
    }
}
