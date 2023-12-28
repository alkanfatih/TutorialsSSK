using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.App.DTOs
{
    public class CommentDTO
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public int ArticleId { get; set; }
        public DateTime CommentDate { get; set; }
    }
}
