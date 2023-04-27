using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Model.Comment
{
    public class CommentInfoModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int ChapterId { get; set; }

        public string Content { get; set; }

        public int Likes { get; set; }
    }
}
