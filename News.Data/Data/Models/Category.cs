using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace News.Data.Data.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
    
        public string CategoryName { get; set; } = string.Empty;

        public List<New>? News { get; set; } = new List<New>();

        public List<Author> Authors{ get; set; } = new List<Author>();
    }
}