using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Model.Entities
{
    public class User : BaseEntity
    {
        [Required]
        [EmailAddress]
        [MaxLength(50)]
        [Column(TypeName ="nvarchar(50)")]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string Username { get; set; }

        [Required]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string Password { get; set; }

        [Required]
        [MaxLength(15)]
        [Column(TypeName = "nvarchar(15)")]
        public string Role { get; set; }

        public virtual ICollection<Book> Books {get;set;} 
        public virtual ICollection<Comment> Comments { get;set;}
        public virtual ICollection<Review> Reviews { get;set;}

        public User()
        {
            Comments= new HashSet<Comment>();
            Books = new HashSet<Book>();
            Reviews= new HashSet<Review>();
            Role = "user";
        }
    }
}
