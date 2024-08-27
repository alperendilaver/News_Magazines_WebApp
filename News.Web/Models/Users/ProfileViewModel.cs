using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using News.Data.Data.Models;

namespace News.Web.Models.Users
{
    public class ProfileViewModel
    {
        public User? User { get; set; }
        public Author? Author { get; set; }
    }
}