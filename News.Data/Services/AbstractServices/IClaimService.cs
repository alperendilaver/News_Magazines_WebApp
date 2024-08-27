using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Data.Services.AbstractServices
{
    public interface IClaimService
    {
        public Task GenerateClaim(int Id,string firstName,string lastName,string PhoneNumber,bool IsPersistent,string role);

        public Task DeleteClaim();
    }
}