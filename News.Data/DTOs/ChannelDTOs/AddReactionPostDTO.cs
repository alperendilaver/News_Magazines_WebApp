using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Data.DTOs.ChannelDTOs
{
    public class AddReactionPostDTO
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public bool IsLike { get; set; }    
    }
}