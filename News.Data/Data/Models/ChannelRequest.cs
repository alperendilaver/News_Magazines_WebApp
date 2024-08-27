using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace News.Data.Data.Models
{
    public class ChannelRequest
    {
        [Key]
        public int RequestId { get; set; }
    
        public int ChannelId { get; set; }
        public Channel Channel { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public bool IsConfirmed { get; set; } = false;
        
    }
}