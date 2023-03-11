namespace DAL.Model.Entities
{
    public class User : BaseEntity
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public virtual ICollection<Book> Books {get;set;} 
        public virtual ICollection<Comment> Comments { get;set;}
        public virtual ICollection<Review> Reviews { get;set;}

        public User()
        {
            Comments= new HashSet<Comment>();
            Books = new HashSet<Book>();
            Reviews= new HashSet<Review>();
        }
    }
}
