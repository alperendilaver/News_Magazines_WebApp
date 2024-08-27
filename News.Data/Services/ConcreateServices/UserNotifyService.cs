using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using News.Data.Data.Context;
using News.Data.Services.AbstractServices;

namespace News.Data.Services.ConcreateServices
{
    public class UserNotifyService : IUserNotifyService
    {
        private NewsContext _newsContext;
        public UserNotifyService(NewsContext newsContext)
        {
            _newsContext=newsContext;
        }
        public async Task<int> GetNotCount(int UserId)
        {
            return await _newsContext.Notifies.CountAsync(x=>x.UserId==UserId);         
        }
    }
}