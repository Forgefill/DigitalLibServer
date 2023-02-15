namespace DigitalLibServer.Model.Entities
{
    public class Review:BaseEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int BookId { get; set; }
        public virtual Book Book { get; set; }

        public string Content { get; set; }

        public int Score { get; set; }

        public int Likes { get; set; }
    }
}
