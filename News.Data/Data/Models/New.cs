using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Data.Data.Models
{
    public class New
    {
        public int NewId { get; set; }
        public string Tittle { get; set; } = string.Empty;

        public string Context { get; set; } = string.Empty;

        public int? UserId { get; set; }
        public Author? User { get; set; }

        public string Image { get; set; } = string.Empty;
        public List<Comment> Comments { get; set; } = new List<Comment>();

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public DateTime PublishedTime{get;set;}

        public bool IsConfirmed{get; set;}

        public bool IsActive { get; set; }

        public int LikeCount { get; set; }

        public int DisslikeCount { get; set; }
        public ICollection<NewRequests> NewRequests { get; set; } = new List<NewRequests>();
        public List<NewReaction> Reactions { get; set; } = new List<NewReaction>();
    }
}