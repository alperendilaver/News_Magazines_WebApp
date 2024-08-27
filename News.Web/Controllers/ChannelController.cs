using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using News.Data.Services.AbstractServices;
using News.Web.Models.Channel;

namespace News.Web.Controllers
{
      public class ChannelController : Controller
    {
        private IUserNotifyService _notifyService;
        private IRequestService _requestService;
        private IChannelService _channelService;
        public ChannelController(IChannelService channelService,IRequestService requestService,IUserNotifyService notifyService)
        {
            _notifyService = notifyService;
            _requestService = requestService;
            _channelService=channelService;
        }
        public async Task<IActionResult> Index(int UserId)
        {
            return View(new IndexViewModel{Channels=await _channelService.GetChannels() ,UserChannelIds = await _channelService.GetUserChannelsId(UserId),UserRequests = await _channelService.GetUserRequestChannelId(UserId)});
        }

        [HttpGet]
        public async Task<IActionResult> GetAllChannels(int UserId){
            var channels=await _channelService.GetChannels();
            var userChannelIds =await _channelService.GetUserChannelsId(UserId);
            var userReq = await _channelService.GetUserRequestChannelId(UserId);
            var model = new IndexViewModel{Channels=channels ,UserChannelIds =userChannelIds ,UserRequests = userReq};
            return PartialView("_GetAllChannelsPartial",model);
        }
        public IActionResult CreateChannel(){
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateChannel(CreateChannelViewModel createChannelViewModel){
            var result = await _channelService.CreateChannel(new Data.DTOs.ChannelDTOs.CreateChannelDTO{AuthorId=createChannelViewModel.AuthorId,ChannelName=createChannelViewModel.ChannelName});
            if(result.Success)
                return RedirectToAction("ChannelDetail","Channel",new {ChannelId = result.Data});
            else
                return View(createChannelViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteChannel(int ChannelId){
            var result = await _channelService.DeleteChannel(ChannelId);
            return RedirectToAction("Profile","Users",new{Id = result.Data});
        }
        public async Task<IActionResult> ChannelDetail(int? ChannelId,int? UserId){
            if(UserId!=null){
                return View(await _channelService.GetChannelByAuthorId(UserId));
            }
            return View(await _channelService.GetChannel(ChannelId));
        }
        [HttpGet]
        public async Task<IActionResult> ReqCount(int AuthorId){
            ViewBag.ReqCount =await _requestService.GetReqCount(AuthorId);
        
            return PartialView("_GetChannelMemberRequestPartial");
        }
        [HttpGet]
        public async Task<IActionResult> NotifyCount(int UserId){
            @ViewBag.NotifyCount = await _notifyService.GetNotCount(UserId);
            return PartialView("_GetNotifyPartial");
        }
        [HttpGet]
        public async Task<IActionResult> MemCount(int ChannelId){
            ViewBag.MemCount = await _channelService.GetMemCount(ChannelId);
            return PartialView("_GetChannelMemberCountPartial");
        }
        [HttpPost]
        public async Task<JsonResult> CreatePost(CreatePostViewModel createPostViewModel){
            var result = await _channelService.CreatePost(new Data.DTOs.ChannelDTOs.CreatePostDTO{
                AuthorId=createPostViewModel.AuthorId,
                Context = createPostViewModel.Context,
                ChannelId = createPostViewModel.ChannelId,
            });
            return Json(new {postId=result.Data.PostId,authorId=result.Data.AuthorId,message=result.Message,channelId=result.Data.ChannelId,context=result.Data.Context,publishedTime=result.Data.PublishedTime.ToString("dd/MM/yyyy HH:mm")});
        }

        [HttpPost]
        public async Task<JsonResult> AddReaction(int postId,string type,int userId){
            var result = await _channelService.AddReactionToPost(new Data.DTOs.ChannelDTOs.AddReactionPostDTO{
                IsLike = true ? type =="true":false,
                PostId = postId,
                UserId = userId,
            });
            return Json(new{result.Data.LikeCount,result.Data.DisslikeCount,message=result.Message});
        }

        [HttpPost]
        public async Task<JsonResult> DeletePost(int postId){
            var result =await _channelService.DeletePost(postId);
            return Json(new{message=result.Message, postId = result.Data});
        }

        [HttpPost]
        public async Task<JsonResult> AddRequest(int ChannelId,int UserId){
            var result = await _requestService.CreateRequest(ChannelId,UserId);
            return Json(new{message=result.Message});
        }

        public async Task<IActionResult> ViewRequests(int? ChannelId,int? UserId){
            if(UserId!=null){
                return View(await _requestService.GetChannelRequests(UserId:UserId,ChannelId:null));
            }
            return View(await _requestService.GetChannelRequests(ChannelId:ChannelId, UserId:null));
        }
        [HttpPost]
        public async Task<JsonResult> ProccessRequest(int ReqId,string type){
            if(type=="true"){
               var result= await _requestService.ConfirmRequestForChannel(ReqId);
                return Json(new{result.Message});
            }
            else{
                var result = await _requestService.RejectRequestForChannel(ReqId);
                return Json(new{message=result.Message});
            }
        }

        public async Task<IActionResult> ViewMembers(int ChannelId){
            ViewBag.ChannelId = ChannelId;
            return View(await _channelService.GetMembers(ChannelId));
        }

        [HttpPost]
        public async Task<JsonResult> RemoveMember(int ChannelId,int MemberId){
            var result = await _channelService.RemoveMember(channelId:ChannelId,userId:MemberId);
            return Json(new{result.Message});
        }
    }
}