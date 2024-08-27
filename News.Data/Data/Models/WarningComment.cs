using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace News.Data.Data.Models
{
    public class WarningComment
    {
        [Key]
        public int WarnId { get; set; }
        public int? CommentId { get; set; }


        [ForeignKey(nameof(CommentId))]
        public Comment? Comment { get; set; } =null!;
        public int? ReplyId { get; set; }

        [ForeignKey(nameof(ReplyId))]
        public CommentReply? CommentReply { get; set; }
        public string Reason { get; set; } = string.Empty;

        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        public string Type { get; set; } = string.Empty;
    }
}