

namespace DAL.Model.Entities
{
    public class Comment:BaseEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int ChapterId { get; set; }
        public virtual Chapter Chapter { get; set; }

        public string Content { get; set; }

        public int Likes { get; set; }
    }
}
