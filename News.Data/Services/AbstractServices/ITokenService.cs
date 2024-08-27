using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Data.Services.AbstractServices
{
    public interface ITokenService
    {
        public Task SaveTokentoDatabase(int UserId,string token,string tokentype,DateTime tokenExpride);

        public Task<bool> VerifyToken(int UserId, string token);
    }
}