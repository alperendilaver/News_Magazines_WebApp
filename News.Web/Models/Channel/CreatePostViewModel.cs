using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Web.Models.Channel
{
    public class CreatePostViewModel
    {
        public string Context { get; set; } =string.Empty;
    
        public int ChannelId { get; set; }
        
        
        public int AuthorId { get; set; }
    }
}