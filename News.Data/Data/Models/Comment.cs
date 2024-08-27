using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Data.Data.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Context { get; set; } = string.Empty;
        public DateTime PublishedDate { get; set; } 
        public int NewId { get; set; }
        public New New { get; set; } = null!;
        public int UserId { get; set; } 
        public User User { get; set; } = null!;

        public int LikeCount { get; set; }
        public int DisslikeCount { get; set; }
        
        public int ReplyCount{get; set; }
        public List<CommentReply>? Replies { get; set; }
        
        public List<WarningComment>? Warnings {get;set;} 
    }
}