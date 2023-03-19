using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Model.Entities
{
    [Table("Books")]
    public class Book:BaseEntity
    {
        [Required]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar2(100)")]
        public string Title { get; set; }

        [MaxLength(1000)]
        [Column(TypeName = "nvarchar2(1000)")]
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
