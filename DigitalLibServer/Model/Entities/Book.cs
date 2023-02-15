
namespace DigitalLibServer.Model.Entities
{
    public class Book:BaseEntity
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public int AuthorId { get; set; }
        public virtual User Author { get; set; }

        public virtual ICollection<Chapter> Chapters { get; set; }  
        public virtual ICollection<BookGenre> BookGenres { get; set; }
        public virtual ICollection<BookTag> BookTags { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }

        public Book() 
        { 
            Chapters = new HashSet<Chapter>();
            BookGenres= new HashSet<BookGenre>();
            BookTags= new HashSet<BookTag>();
            Reviews = new HashSet<Review>();
        }
    }
}
