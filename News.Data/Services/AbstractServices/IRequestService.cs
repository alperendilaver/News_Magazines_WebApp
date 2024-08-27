using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using News.Data.Data.Models;

namespace News.Data.Services.AbstractServices
{
    public interface IRequestService
    {
        public Task<GeneralResponse<int>> CreateRequest (int NewId,string type);

        public Task<GeneralResponse<int>> CreateRequest(int ChannelId,int UserId);
        public Task<GeneralResponse<int>> ConfirmRequestForPublishNew(int NewId,string type);
        public  Task<GeneralResponse<int>> CreateRequestForCategory(UserCategoryRequest userCategoryRequest);
    
        public Task<List<NewRequests>> GetNewRequests(string type);
        public Task<List<UserCategoryRequest>> GetCatRequests();

        public Task<List<ChannelRequest>> GetChannelRequests(int? ChannelId,int? UserId);

        public Task<int> GetReqCount(int ChannelId);

        public Task<GeneralResponse<int>> ConfirmRequestForChangeCategory(int UserId,int role);

        public Task<GeneralResponse<int>> DeleteRequest(int Id);

        public Task<GeneralResponse<int>> ConfirmRequestForChannel(int reqId);
        public Task<GeneralResponse<int>> RejectRequestForChannel(int reqId);
     
    }
}