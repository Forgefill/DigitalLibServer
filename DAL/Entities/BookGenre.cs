using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class BookGenre:BaseEntity
    {
        [Required]
        public int BookId { get; set; }
        public virtual Book Book { get; set; }

        [Required]
        public int GenreId { get; set; }
        public virtual Genre Genre { get; set;}
    }
}
