using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace News.Data.Data.Models
{
    public class UserTokens
    {
        [Key]
        public int UserTokensId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public string Token { get; set; } = string.Empty;
        public string TokenType { get; set; } = string.Empty;
        public DateTime TokenExpirationTime { get; set; }

        public bool IsProceed { get; set; }

        public bool IsVerified { get; set; }
    }
}