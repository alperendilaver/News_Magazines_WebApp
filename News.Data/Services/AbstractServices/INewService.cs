using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using News.Data.Data.Models;
using News.Data.DTOs.NewDTOs;


namespace News.Data.Services.AbstractServices
{
    public interface INewService
    {
        public Task<GeneralResponse<int>> CreateNew(New entity);

        public IQueryable<New> GetNews();

        public Task<List<New>> GetAllNews();

        public Task<List<New>> GetConfirmedNews();
        public Task<New> GetNewForDetail(int Id);
        public Task<New> GetNewForEdit(int Id);
        public Task<List<New>> GetUserNews(int Id);
        public Task<List<New>> GetNewForCategoryDetail(int Id);
        public Task<GeneralResponse<int>> UpdateNew(EditNewDTO entity);

        public Task<GeneralResponse<int>> DeleteNew(int NewId);

        public Task<GeneralResponse<int>> PublishNew(int NewId);

        public  Task<GeneralResponse<int>> UnpublishNew(int NewId);

        public Task<List<New>> GetFilteredNews(string filter);

        public Task<GetRandomNewDTO> GetRandomNews();

        public Task<RandomNewDTO> GetRandomNew();

        public Task<GeneralResponse<Comment>> CreateComment(CreateCommentDTO createCommentDTO);

        public Task<List<Comment>> GetCommentsForNew(int NewId);

        public Task<GeneralResponse<KeepReactionCount>> AddReactionToComment(int CommentId,bool reactionType,string comtype);

        public Task<GeneralResponse<CommentReply>> AddReplyToComment(AddReplyDTO replyDTO);

        public Task<List<Comment>> GetComments();

        public Task<GeneralResponse<int>> DeleteComment(int CommentId ,string type,int? WarningId);

        public Task<GeneralResponse<New>> AddReaction(AddReactionDTO addReactionDTO);
    }
}