using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Web.Models.New
{
    public class EditNewViewModel
    {
        public int NewId { get; set; }
        public string Tittle { get; set; } = string.Empty;
        public string Context { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public IFormFile? ImageFile{ get; set; }
        
        public int CategoryId{ get; set; }
    }
}