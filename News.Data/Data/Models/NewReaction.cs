using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace News.Data.Data.Models
{
    public class NewReaction
    {
        [Key]
        public int ReactionId { get; set; }
        
        public int NewId { get; set; }
        public New New { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public bool IsLike { get; set; }
    }
}