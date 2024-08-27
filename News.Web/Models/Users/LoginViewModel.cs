using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Web.Models.Users
{
    public class LoginViewModel
    {
        public string Mail { get; set; }=string.Empty;

        public string Password { get; set; }=string.Empty;

        public bool RememberMe { get; set; }
 
    }
}