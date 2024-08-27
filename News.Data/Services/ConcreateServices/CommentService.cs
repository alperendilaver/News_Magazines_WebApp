using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using News.Data.Data.Context;
using News.Data.Data.Models;
using News.Data.DTOs.NewDTOs;
using News.Data.Services.AbstractServices;

namespace News.Data.Services.ConcreateServices
{
    public class CommentService : ICommentService
    {
        private NewsContext _newsContext;

        public CommentService(NewsContext newsContext)
        {
            _newsContext = newsContext;
        }

        public async Task<List<WarningComment>> GetWarnings()
        {
            return await _newsContext.WarningComments.Include(x=>x.User).Include(x=>x.Comment).ThenInclude(x=>x.User).Include(x=>x.CommentReply).ToListAsync();
        }

        public async Task<GeneralResponse<int>> SendWarningForComment(SendWarningForCommentDTO warningForCommentDTO)
        {
            var response = new GeneralResponse<int>();
            var Warning = new WarningComment();
            if(warningForCommentDTO.type == "comment")
                Warning.CommentId = warningForCommentDTO.CommentId;
            else
                Warning.ReplyId = warningForCommentDTO.CommentId;
            try
            {
                Warning.UserId = warningForCommentDTO.UserId;
                Warning.Reason = warningForCommentDTO.reason;
                Warning.Type = warningForCommentDTO.type;
                await _newsContext.WarningComments.AddAsync(Warning);
                await _newsContext.SaveChangesAsync();
                response.Success = true;
                response.Message = "Yorum Başarı ile Bildirildi";
            }
            catch (System.Exception ex)
            {
                response.Message ="Yorum Bildirilemedi";
                response.Success = false;
                response.Errors.Add(ex.Message);
            }
            return response;
        }

        public async Task<GeneralResponse<int>> RemoveWarning(int WarningId){
            var response = new GeneralResponse<int>();  
            try
            {
                var warning = _newsContext.WarningComments.FirstOrDefault(x => x.WarnId==WarningId);
                if(warning!=null){
                    _newsContext.WarningComments.Remove(warning);
                    await _newsContext.SaveChangesAsync();
                    response.Success=true;
                    response.Message = "Başarı İle Silindi";
                    response.Data = warning.WarnId;
                    return response;
                }

            }
            catch (System.Exception ex)
            {
                response.Errors.Add(ex.Message);
                response.Success = false;
                response.Message ="Başarısız silme";
            }
            return response;
        }
    }

    //reply ise reply comment ise comment'e kaydedilecek
}