using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using News.Data.Data.Models;
using News.Data.DTOs.NewDTOs;

namespace News.Data.Services.AbstractServices
{
    public interface ICommentService
    {
        public Task<GeneralResponse<int>> SendWarningForComment(SendWarningForCommentDTO warningForCommentDTO);

        public Task<List<WarningComment>> GetWarnings();

        public Task<GeneralResponse<int>> RemoveWarning(int WarningId);
    }
}