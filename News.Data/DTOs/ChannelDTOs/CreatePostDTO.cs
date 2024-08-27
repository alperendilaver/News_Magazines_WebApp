using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Data.DTOs.ChannelDTOs
{
    public class CreatePostDTO
    {
        public int ChannelId { get; set; }
        public string Context { get; set; } = string.Empty;
        public int AuthorId { get; set; }
        
    }
}