using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Data.DTOs.NewDTOs
{
    public class CreateNewDTO
    {
        public int UserId { get; set; }

        public int CategoryId { get; set; }
        public string Context = string.Empty;
        public string Tittle = string.Empty;
        public string Image = string.Empty;
        public DateTime PublishedTime = DateTime.Now;
        public bool IsActive = true;
    }
}