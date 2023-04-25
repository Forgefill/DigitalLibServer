using DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
