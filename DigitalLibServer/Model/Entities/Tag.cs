
namespace DigitalLibServer.Model.Entities
{
    public class Tag:BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<BookTag> BookTags { get; set; }

        public Tag()
        {
            BookTags = new HashSet<BookTag>();
        }
    }
}
