using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Image: BaseEntity
    {
        [Required]
        public int BookId { get; set; }
        public virtual Book Book { get; set; }

        public string ContentType { get; set; }

        public byte[] ImageData { get; set; }
    }
}
