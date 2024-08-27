using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using News.Data.Data.Context;
using News.Data.Data.Models;
using News.Data.Services.AbstractServices;

namespace News.Data.Services.ConcreateServices
{
    public class TokenService : ITokenService
    {
        private NewsContext _newsContext;
        public TokenService(NewsContext newsContext)
        {
            _newsContext=newsContext;
        }
        public async Task SaveTokentoDatabase(int UserId, string token, string tokentype, DateTime tokenExpride)
        {
            var entity = new UserTokens{
                UserId = UserId,
                Token = token,
                TokenType = tokentype,
                TokenExpirationTime = tokenExpride
            };
            await _newsContext.AddAsync(entity);
            await _newsContext.SaveChangesAsync();
        }

        public async Task<bool> VerifyToken(int UserId, string token)
        {
            var entity = await _newsContext.UserTokens.Where(x=>x.UserId==UserId & x.Token == token && x.TokenExpirationTime>DateTime.UtcNow&&x.IsVerified==false).Include(u=>u.User).FirstOrDefaultAsync();
            if (entity==null)
                return false;
            entity.IsVerified = true;
            entity.IsProceed  = true;
            entity.User.IsEmailConfirmed =true;
            var result = await _newsContext.SaveChangesAsync();
            return true ? result>0 :false;
        }   
    }
}