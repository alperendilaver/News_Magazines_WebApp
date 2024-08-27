using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace News.Data.Data.Models
{
    public class Author
    {
       [Key]
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;		
        public List<New> News { get; set; } = new List<New>();    
        public List<Category>? Category { get; set; } = new List<Category>();
        public string Unvan { get; set; }=string.Empty;

        public int? ChannelId { get; set; }
        public Channel? Channel { get; set; }
    }
}