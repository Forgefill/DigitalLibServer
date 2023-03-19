using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Model.Entities
{
    public class Review:BaseEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int BookId { get; set; }
        public virtual Book Book { get; set; }

        [Required]
        [Column(TypeName ="NCLOB")]
        public string Content { get; set; }

        [Required]
        [DefaultValue(0)]
        public int Score { get; set; }

        [DefaultValue(0)]
        public int Likes { get; set; }
    }
}
