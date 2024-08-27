using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using News.Data.Data.Models;

namespace News.Web.Models.New
{
    public class DetailViewModel
    {
        public News.Data.Data.Models.New New { get; set; } = null!;
        public List<Data.Data.Models.New> RandomNews { get; set; } = new List<Data.Data.Models.New>();

        public List<Data.Data.Models.Comment> Comments{ get; set; } = new List<Data.Data.Models.Comment>();

    }
}