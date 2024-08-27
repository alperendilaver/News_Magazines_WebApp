using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace News.Data.Data.Models
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PostId { get; set; }

        public int AuthorId { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public Author Author { get; set; } =null!;
        
        public int LikeCount { get; set; }
        public int DisslikeCount { get; set; }
        public string Context { get; set; } = string.Empty;
        public int ChannelId { get; set; }

        [ForeignKey(nameof(ChannelId))]
        public Channel Channel { get; set; }=null!;
        public DateTime PublishedTime { get; set; }
        public List<PostReaction>? Reactions { get; set; } = new List<PostReaction>();
    }
}