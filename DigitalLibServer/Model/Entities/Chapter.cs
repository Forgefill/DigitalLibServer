namespace DigitalLibServer.Model.Entities
{
    public class Chapter:BaseEntity
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public int ChapterNum { get; set; }

        public int BookId { get; set; }
        public virtual Book Book { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public Chapter() 
        { 
            Comments = new HashSet<Comment>();
        }    
    }
}
