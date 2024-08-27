using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using News.Data.Services.AbstractServices;

namespace News.Data.Services.ConcreateServices
{
    public class HashService : IHashService
    {
        private static HashService _instance;
        private static readonly object _lock = new object();
        private HashService()
        {
        }
        public static HashService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new HashService();
                        }
                    }
                }
                return _instance;
            }
        }
        public string Hashing(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2")); // Hexadecimal format
                    }
                return builder.ToString();
            }
        }

        public bool Verify(string InputHashedPassword, string TrueHashedPassword)
        {
            if(string.IsNullOrEmpty(InputHashedPassword)|| string.IsNullOrEmpty(TrueHashedPassword))
                return false;
            
            else if(InputHashedPassword == TrueHashedPassword)
                return true;
            
            return false;
        }

        public string Hashing(string input,string type)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input+type));
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2")); // Hexadecimal format
                    }
                return builder.ToString();
            }
        }

        public int HashingForPostId(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            // Convert the first 4 bytes of the hash to an integer
            int hashInteger = BitConverter.ToInt16(hashBytes, 0);
            return Math.Abs(hashInteger); // Ensure the integer is positive
        }
        }
    }
}