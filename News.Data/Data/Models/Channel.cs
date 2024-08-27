using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace News.Data.Data.Models
{
    public class Channel
    {
        [Key]
        public int ChannelId { get; set; }
        public string ChannelName { get; set; } = string.Empty;
        public int AuthorId { get; set; }
        [ForeignKey(nameof(AuthorId))]
        public Author Author { get; set; } = null!;
        public List<Post>? Posts { get; set; } 
        public List<User>? Members { get; set; } 
        public List<ChannelRequest>? Requests { get; set; }
     }
}