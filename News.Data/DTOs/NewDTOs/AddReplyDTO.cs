using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Data.DTOs.NewDTOs
{
    public class AddReplyDTO
    {
        public int UserId { get; set; }
        public int CommentId { get; set; }
        public string Context { get; set; } = string.Empty;

        public string Comment { get; set; } = string.Empty;

        public int NewId { get; set; }
        public int CommentUserId { get; set; }

        public string UserName { get; set; }=string.Empty;
    }
}