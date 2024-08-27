using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Web.Models.Requests
{
    public class ChangeCategoryRequestDTO
    {
        public int UserId { get; set; }
        public int CatId { get; set; }
    }
}