using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using News.Data.Services.AbstractServices;
namespace News.Data.Services.ConcreateServices
{
    public class ClaimService : IClaimService
    {
      
       
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ClaimService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task DeleteClaim()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);   
        }

        public  async Task GenerateClaim(int Id, string firstName, string lastName, string PhoneNumber, bool RememberMe,string role)
        {
            var userClaims = new List<Claim>();
            userClaims.Add(new Claim(ClaimTypes.NameIdentifier,Id.ToString()));
            userClaims.Add(new Claim(ClaimTypes.Name,firstName));
            userClaims.Add(new Claim(ClaimTypes.GivenName,lastName));
            userClaims.Add(new Claim(ClaimTypes.UserData,PhoneNumber));
            userClaims.Add(new Claim(ClaimTypes.Role,role));
            var claimsIdentity = new ClaimsIdentity(userClaims,CookieAuthenticationDefaults.AuthenticationScheme);
            
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = RememberMe,
                ExpiresUtc = RememberMe 
                    ? DateTimeOffset.UtcNow.AddDays(1) // "Beni Hatırla" seçili ise 1 gün
                    : DateTimeOffset.UtcNow.AddHours(1) // Seçili değilse 1 saat
            };

            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);   
            

            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(claimsIdentity),authProperties);
        }
    }
}