using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using News.Data.Data.Models;


namespace News.Web.Models.Home
{
    public class ShowNewsViewModel
    {
        public List<News.Data.Data.Models.New> AllNews { get; set; } = new List<News.Data.Data.Models.New>();

        public List<News.Data.Data.Models.New> FirstRandomCategoryNews { get; set; } = new List<News.Data.Data.Models.New>();

        public List<News.Data.Data.Models.New> SecondRandomCategoryNews { get; set; } = new List<News.Data.Data.Models.New>();

        public List<Author> Authors { get; set; } = new List<Author>();

    }
}