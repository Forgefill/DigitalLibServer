using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Model.Entities
{
    public class Tag:BaseEntity
    {
        [Required]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar2(100)")]
        public string Name { get; set; }

        public virtual ICollection<BookTag> BookTags { get; set; }

        public Tag()
        {
            BookTags = new HashSet<BookTag>();
        }
    }
}
