
namespace BLL.Model.Comment
{
    public class CommentModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int ChapterId { get; set; }

        public string Content { get; set; }
    }
}
