using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using News.Data.Data.Models;

namespace News.Web.Models.Channel
{
    public class IndexViewModel
    {
        public List<Data.Data.Models.Channel> Channels { get; set; }  = new List<Data.Data.Models.Channel>();
        public List<int> UserChannelIds { get; set; } = new List<int>();
        public List<int> UserRequests { get; set; } = new List<int>();
    }
}