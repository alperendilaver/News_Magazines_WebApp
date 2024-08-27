using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace News.Data.Data.Models
{
    public class CommentReply
    {
        [Key]
        public int ReplyId { get; set; }

        public int CommentId { get; set; }
        
        public Comment Comment { get; set; } = null!;
        public string Text { get; set; } = string.Empty;
        public DateTime PublishedDate { get; set; } 
        public int UserId { get; set; } 
  
        public User User { get; set; } = null!;

        public int LikeCount { get; set; }
        public int DisslikeCount { get; set; }
        public List<WarningComment>? Warnings {get;set;} 

    }
}