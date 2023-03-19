using System.ComponentModel.DataAnnotations;

namespace DAL.Model.Entities
{
    public class BookTag:BaseEntity
    {
        [Required]
        public int BookId { get; set; }
        public virtual Book Book { get; set; }

        [Required]
        public int TagId { get; set; }
        public virtual Tag Tag { get; set; }

    }
}
