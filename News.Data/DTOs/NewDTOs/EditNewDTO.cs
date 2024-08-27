using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace News.Data.DTOs.NewDTOs
{
    public class EditNewDTO
    {
        public int NewId { get; set; }
        public string Tittle { get; set; } = string.Empty;
        public string Context { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public IFormFile? ImageFile{ get; set; }
        
        public int CategoryId{ get; set; }
    
    }
}