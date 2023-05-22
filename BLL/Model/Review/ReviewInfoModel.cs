
namespace BLL.Model.Review
{
    public class ReviewInfoModel
    {
        public int UserId { get; set; }

        public int BookId { get; set; }

        public string Content { get; set; }

        public int Score { get; set; }

        public int Likes { get; set; }
    }
}
