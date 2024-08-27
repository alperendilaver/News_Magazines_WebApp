using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Data.DTOs.NewDTOs
{
    public class SendWarningForCommentDTO
    {
        public int CommentId { get; set; }
        public string reason { get; set; } = string.Empty;

        public int UserId { get; set; }

        public string type { get; set; }= string.Empty;
    }
}