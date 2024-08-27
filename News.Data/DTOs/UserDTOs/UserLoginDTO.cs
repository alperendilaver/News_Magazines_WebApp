using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Data.DTOs.UserDTOs
{
    public class UserLoginDTO
    {
        public string Mail { get; set; }=string.Empty;

        public string Password { get; set; }=string.Empty;

        public bool RememberMe { get; set; }
        public bool IsPersistent { get; set; }
    }
}