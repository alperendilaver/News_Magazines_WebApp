using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using News.Data.Data.Models;

namespace News.Data.DTOs.NewDTOs
{
    public class RandomNewDTO
    {
        public List<New> RandomNews { get; set; } = new List<New>();
    }
}