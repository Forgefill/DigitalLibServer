using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Model
{
    public class BookModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public int AuthorId { get; set; }
        public string AuthorUsername { get; set; }  

        public int? ImageId { get; set; }
        
        public double AverageScore { get; set; } = 0;
    }
}
