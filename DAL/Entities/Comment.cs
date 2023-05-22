using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Comment:BaseEntity
    {
        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [Required]
        public int ChapterId { get; set; }
        public virtual Chapter Chapter { get; set; }

        [Required]
        [MaxLength(1000)]
        [Column(TypeName ="nvarchar(1000)")]
        public string Content { get; set; }

        [DefaultValue(0)]
        public int Likes { get; set; }

        [DefaultValue(false)]
        public bool IsReported { get; set; }
    }
}
