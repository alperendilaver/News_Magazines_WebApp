using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Data.DTOs.NewDTOs
{
    public class AddReactionDTO
    {
        public int NewId { get; set; }

        public int UserId { get; set; }
        public bool IsLike { get; set; }
    }
}