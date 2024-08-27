using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.EntityFrameworkCore;
using News.Data.Data.Context;
using News.Data.Data.Models;
using News.Data.Services.AbstractServices;

namespace News.Data.Services.ConcreateServices
{
    public class RequestService : IRequestService
    {
        private IEmailService _emailService;
        private readonly INotyfService _notyf;

        private readonly NewsContext _context;
        public RequestService(NewsContext newsContext,INotyfService notyf,IEmailService emailService)
        {
            _emailService=emailService;
            _notyf = notyf;
            _context = newsContext;
        }
        public async Task<GeneralResponse<int>> ConfirmRequestForPublishNew(int NewId, string type)
        {
            var response = new GeneralResponse<int>();
            var request = await _context.NewRequests.Include(x=>x.New).FirstOrDefaultAsync(x => x.NewId == NewId && x.RequestType == type);
            if(request == null){  
                response.Success = false;
                response.Message = "İstek bulunamadı";
                return response;  
            }
            try
            {
                request.RequestType = "confirmed";
                request.New.IsConfirmed = true;
                request.New.PublishedTime =DateTime.UtcNow.AddHours(3);
                var resultSave= await _context.SaveChangesAsync();
                response.Success = true;
                response.Message = "İstek Onaylandı";
                response.Data = resultSave;    
            }
            catch (System.Exception ex)
            {
                response.Success = false;
                response.Message = "İstek onaylanamadı";
                response.Errors.Add(ex.Message);
            }
            return response;
        }

        public async Task<GeneralResponse<int>> CreateRequest(int NewId, string type)
        {
            var response = new GeneralResponse<int>();
            if(_context.NewRequests.Any(x=>x.NewId == NewId && x.RequestType == type))
            {
                response.Message = "Zaten Bekleyen bir isteğiniz var";
                response.Success = false;
                response.Data = 0;
                _notyf.Error(response.Message);
                return response;
            }
            try
            {
                await _context.NewRequests.AddAsync(new NewRequests{
                    NewId=NewId,
                    RequestType=type
                });
                var resultSave=await _context.SaveChangesAsync();
                response.Success =true;
                response.Message = "İstek oluşturuldu";
                response.Data = resultSave;    
            }
            catch (System.Exception ex)
            {
                response.Message = "İstek oluşturulamadı";
                response.Success =false;
                response.Errors.Add(ex.Message);
                response.Data = 0;
            }
            return response;
        }

        public async Task<List<UserCategoryRequest>> GetCatRequests(){
            return await _context.UserCategoryRequests.Include(x=>x.Category).Include(x=>x.User).ToListAsync();
        }

        public async Task<GeneralResponse<int>> CreateRequestForCategory(UserCategoryRequest userCategoryRequest){
            var response = new GeneralResponse<int>();
            try
            {
                await _context.UserCategoryRequests.AddAsync(userCategoryRequest);
                var resultSave =  await _context.SaveChangesAsync();
                response.Success=true;
                response.Message = "Talep Admine iletildi";
                response.Data = resultSave;
            }
            catch (System.Exception ex)
            {
                response.Success=false;
                response.Message = "Talep Admine iletilmedi tekrar deneyiniz";
                response.Errors.Add(ex.Message);
            }
            return response;
        }

        public async Task<GeneralResponse<int>> ConfirmRequestForChangeCategory(int UserId, int role)
        {
            var response = new GeneralResponse<int>();
            var user = await _context.Users.Include(x=>x.Author).FirstOrDefaultAsync(x=>x.UserId==UserId);
            if (user==null || user.Author==null){
                response.Success = false;
                response.Message = "Kullanıcı bulunamadı";
                return response;
            }
            try
            {
                var cat = _context.Categories.FirstOrDefault(x=>x.CategoryId ==role);
                user.Author.Category.Add(cat);
                _context.UserCategoryRequests.Remove(await _context.UserCategoryRequests.Where(x=>x.UserId==UserId).FirstOrDefaultAsync());
                var resultSave= await _context.SaveChangesAsync();
            
                response.Success=true;
                response.Message = "Kategori Değişti";
                response.Data = resultSave;

            }
            catch (System.Exception ex)
            {
                response.Errors.Add(ex.Message);
                response.Success = false;
                response.Message ="Kategori Değişmedi";
            }
            return response;
        }

        public async Task<List<NewRequests>> GetNewRequests(string type)
        {
            return await _context.NewRequests.Where(x=>x.RequestType==type).Include(x=>x.New).ThenInclude(x=>x.User.User).ToListAsync();
        }

        public async Task<GeneralResponse<int>> DeleteRequest(int ReqId)
        {
            var response = new GeneralResponse<int>();
            var request = await _context.UserCategoryRequests.Where(x=>x.RequestId ==ReqId).FirstOrDefaultAsync();
            if (request==null){
                response.Success=false;
                response.Message = "İstek Bulunamadı";
                return response;
            }
            else{
                try
                {
                    _context.UserCategoryRequests.Remove(request);
                    await _context.SaveChangesAsync();
                    response.Success=true;
                    response.Message = "İstek Silindi";    
                }
                catch (System.Exception ex)
                {
                    response.Success = false;
                    response.Message = "İstek Silinemedi";
                    response.Errors.Add(ex.Message);
                }
                
                return response;
            }
        }

        public async Task<GeneralResponse<int>> CreateRequest(int ChannelId, int UserId)
        {
            var response = new GeneralResponse<int>();
            try
            {
                var request = new ChannelRequest{
                    ChannelId=ChannelId,
                    UserId=UserId,
                    IsConfirmed=false
                };
                await _context.ChannelRequests.AddAsync(request);
                await _context.SaveChangesAsync();
                var query =  _context.ChannelRequests.Where(x=>x.RequestId == request.RequestId).AsQueryable();
                
                var email = await query.Include(x=>x.Channel).ThenInclude(x=>x.Author).ThenInclude(x=>x.User).Select(x=>x.Channel.Author.User.Email).FirstOrDefaultAsync();
                
                var name = await query.Include(x=>x.User).Include(x=>x.User).Select(x=>x.User.FirstName + x.User.LastName).FirstOrDefaultAsync();
                var receiverId = await _context.Channels.Where(x=>x.ChannelId == ChannelId).Select(x=>x.AuthorId).FirstOrDefaultAsync();
                await _emailService.SendEmailAsync(email,"Kanal Katılma İsteği",$"{name} adlı kullanıcı kanalınıza üye olmak için istek gönderdi görüntülemek için lütfen <a href='http://localhost:5254/Channel/ViewRequests?ChannelId={ChannelId}'>tıklayın.</a>");
                var Notify = new Notify{
                    Context = $"{name} adlı kullanıcı kanalınıza katılmak için üyelik isteği gönderdi görüntülemek için tıkla <a href='http://localhost:5254/Channel/ViewRequests?ChannelId={ChannelId}'>tıklayın.</a>",
                    Query ="http://localhost:5254/Channel/ViewRequests?ChannelId={ChannelId}",
                    ViewId = ChannelId,
                    UserId=receiverId
                };
                await _context.Notifies.AddAsync(Notify);
                await _context.SaveChangesAsync();
                response.Message="Katılma İsteği Yazara Gönderildi";
                response.Success=true;
                response.Data = request.RequestId;
                
            }
            catch (System.Exception ex)
            {
                response.Errors.Add(ex.Message);
                response.Success=false;
                response.Message="Katılma isteği yazara gönderilemedi";
            }
            return response;
        }

        public async Task<List<ChannelRequest>> GetChannelRequests(int? ChannelId,int? UserId)
        {
            if(UserId!=null)    
                return await _context.ChannelRequests.Include(x=>x.Channel).Where(x=>x.Channel.AuthorId == UserId &&x.IsConfirmed==false).Include(x=>x.User).Include(x=>x.Channel).ToListAsync();
            return await _context.ChannelRequests.Where(x=>x.ChannelId == ChannelId &&x.IsConfirmed==false).Include(x=>x.User).Include(x=>x.Channel).ToListAsync();
        }

        public async Task<GeneralResponse<int>> ConfirmRequestForChannel(int reqId)
        {
            var response = new GeneralResponse<int>();
            try
            {
                var req = await _context.ChannelRequests.FirstOrDefaultAsync(x=>x.RequestId==reqId);
                req.IsConfirmed = true;
                var user = await _context.Users.FindAsync(req.UserId);
                var channel = await _context.Channels.Include(x=>x.Members).FirstOrDefaultAsync(x=>x.ChannelId==req.ChannelId);
                channel.Members.Add(user);
    
                await _context.SaveChangesAsync();
                _context.ChannelRequests.Remove(req);
                await _context.SaveChangesAsync();
                response.Message="Kullanıcı Kanala Eklendi";
                response.Success = true;
            }
            catch (System.Exception ex)
            {
                response.Success = false;
                response.Message="Kullanıcı Kanala Eklenemedi";
                response.Errors.Add(ex.Message);
              
            }
            return response;
        }

        public async Task<GeneralResponse<int>> RejectRequestForChannel(int reqId)
        {
            var response = new GeneralResponse<int>();

            try
            {
                var req = await _context.ChannelRequests.FirstOrDefaultAsync(x=>x.RequestId == reqId);
                _context.ChannelRequests.Remove(req);
                await _context.SaveChangesAsync();
                response.Success = true;
                response.Message = "İstek Reddedildi";

            }
            catch (System.Exception ex)
            {
                response.Errors.Add(ex.Message);
                response.Success = false;
                response.Message="İstek Reddedilemedi";                
            }
            return response;
        }

        public Task<int> GetReqCount(int AuthorId)
        {
            return _context.ChannelRequests.Include(x=>x.Channel).CountAsync(u=>u.Channel.AuthorId==AuthorId && u.IsConfirmed==false);
        }
    }
}