using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace News.Data.Data.Models
{
    public class Notify
    {   
        [Key]
        public int NotifyId { get; set; }
    
        public string Context { get; set; } = string.Empty;

        public int ViewId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public string Query { get; set; } = string.Empty;

        public bool IsSeen { get; set; } =false;
    }
}