using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Genre : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        public virtual ICollection<BookGenre> BookGenres { get; set; }

        public Genre()
        {
            BookGenres = new HashSet<BookGenre>();
        }
    }
}
