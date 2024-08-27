using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Data.Services.AbstractServices
{
    public interface IHashService
    {
        public string Hashing(string password);
        
        public string Hashing(string userName,string type);

        public int HashingForPostId(string input);

        public bool Verify(string InputHashedPassword,string TrueHashedPassword);
    }
}