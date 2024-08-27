using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using News.Data.Data.Models;
namespace News.Web.Models.Editors
{
    public class EditorsListViewModel
    {
        public List<User> Editors { get; set; } = new List<User>();

        public List<User> Readers { get; set; } = new List<User>();
    
        public List<User> Authors { get; set; } = new List<User>();
    
    }
}