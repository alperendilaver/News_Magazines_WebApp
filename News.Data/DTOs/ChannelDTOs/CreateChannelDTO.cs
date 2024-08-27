using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Data.DTOs.ChannelDTOs
{
    public class CreateChannelDTO
    {
        public string ChannelName { get; set; } = string.Empty;

        public int AuthorId { get; set; }

        
    }
}