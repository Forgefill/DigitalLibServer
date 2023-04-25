using DAL.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("Books")]
    public class Book:BaseEntity
    {
        [Required]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string Title { get; set; }

        [MaxLength(1000)]
        [Column(TypeName = "nvarchar(1000)")]
        public string? Description { get; set; }

        public int AuthorId { get; set; }
        [ForeignKey(nameof(AuthorId))]
        public virtual User Author { get; set; }

        public int? ImageId { get; set; }
        public virtual Image Image { get; set; }

        public virtual ICollection<Chapter> Chapters { get; set; }  
        public virtual ICollection<BookGenre> BookGenres { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }

        public Book() 
        { 
            Chapters = new HashSet<Chapter>();
            BookGenres= new HashSet<BookGenre>();
            Reviews = new HashSet<Review>();
        }
    }
}
