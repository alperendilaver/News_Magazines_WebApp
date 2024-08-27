using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Web.Models.Channel
{
    public class CreateChannelViewModel
    {
        public string ChannelName { get; set; } = string.Empty;

        public int AuthorId { get; set; }

        
    }
}