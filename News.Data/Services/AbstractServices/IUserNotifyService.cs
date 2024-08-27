using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Data.Services.AbstractServices
{
    public interface IUserNotifyService
    {
        public Task<int> GetNotCount(int UserId);
    }
}