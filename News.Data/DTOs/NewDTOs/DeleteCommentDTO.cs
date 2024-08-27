using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Data.DTOs.NewDTOs
{
    public class DeleteCommentDTO
    {
        public int CommentId {get; set;}
        public string Type {get;set;}= string.Empty;

        public int? WarningId { get; set; }
    }
}