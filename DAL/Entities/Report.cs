using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace DAL.Entities
{
    public class Report:BaseEntity
    {
        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Text { get; set; }

        [Required]
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
    }
}
