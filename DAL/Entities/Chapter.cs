using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Model.Entities
{
    public class Chapter:BaseEntity
    {
        [Required]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Content { get; set; }

        [Required]
        public int ChapterNum { get; set; }

        [Required]
        public int BookId { get; set; }
        public virtual Book Book { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public Chapter() 
        { 
            Comments = new HashSet<Comment>();
        }    
    }
}
