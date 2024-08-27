using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Web.Models.New
{
    public class CreateCommentViewModel
    {
    

        public string Context { get; set; } = string.Empty;

        public string PublishedDate { get; set; } = string.Empty;

        public int NewId { get; set; }
        
        public int UserId { get; set; } 
    }
}