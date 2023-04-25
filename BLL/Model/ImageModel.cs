using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
