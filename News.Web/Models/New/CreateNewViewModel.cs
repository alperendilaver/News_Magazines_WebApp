using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using News.Web.Models.Home;

namespace News.Web.Models.New
{
    public class CreateNewViewModel
    {
        public int AuthorId { get; set; }
        public string tittle  { get; set; } = string.Empty;
        public string context{ get; set; }= string.Empty;
        public int CategoryId{ get; set; }
        public string Image{ get; set; }= string.Empty;
        public IFormFile ImageFile{ get; set; } = null!;
    }
}