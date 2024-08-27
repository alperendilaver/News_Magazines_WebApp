using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace News.Data.Data.Models
{
    public class NewRequests
    {
        [Key]
        public int RequestId { get; set; }
        public int NewId { get; set; }
        public New New { get; set; } = null!;
        public string RequestType { get; set; } = string.Empty;

    }
}