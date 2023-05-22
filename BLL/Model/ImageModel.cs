
namespace BLL.Model
{
    public class ImageModel
    {
        public int Id { get; set; }

        public string ContentType { get; set; }

        public int BookId { get; set; }

        public byte[] ImageData { get; set; }
    }
}
