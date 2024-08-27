using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using News.Data.Data.Models;

namespace News.Data.DTOs.NewDTOs
{
    public class GetRandomNewDTO
    {
        public List<New> first { get; set; } = new List<New>();
        public List<New> second { get; set; } = new List<New>();
    }
}