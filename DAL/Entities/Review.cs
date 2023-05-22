using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Review:BaseEntity
    {
        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [Required]
        public int BookId { get; set; }
        public virtual Book Book { get; set; }

        [Required]
        [Column(TypeName ="nvarchar(max)")]
        public string Content { get; set; }

        [Required]
        [DefaultValue(0)]
        public int Score { get; set; }

        [DefaultValue(0)]
        public int Likes { get; set; }

        [DefaultValue(false)]
        public bool IsReported { get; set; }
    }
}
