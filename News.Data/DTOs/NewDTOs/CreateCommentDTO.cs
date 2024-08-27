using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Data.DTOs.NewDTOs
{
    public class CreateCommentDTO
    {
        public string Context { get; set; } = string.Empty;

        public DateTime PublishedDate { get; set; } 

        public int NewId { get; set; }
        
        public int UserId { get; set; } 
    }
}