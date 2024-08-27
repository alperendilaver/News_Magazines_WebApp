using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace News.Data.Data.Models
{
    public class OldPasswords
    {
        [Key]
        public int UserPasswordID { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}