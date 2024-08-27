using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using News.Data.Data.Models;

namespace News.Web.Models.Requests
{
    public class RequestListViewModel
    {
        public List<NewRequests> WaitForPublish { get; set; } = new List<NewRequests>();

        public List<NewRequests> WaitForUpdate { get; set; } = new List<NewRequests>();

        public List<UserCategoryRequest> CategoryRequests{ get; set; } = new List<UserCategoryRequest>();

    }
}