using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using News.Data.Data.Context;
using News.Data.Data.Models;
using News.Data.DTOs.NewDTOs;
using News.Data.Services.AbstractServices;

namespace News.Data.Services.ConcreateServices
{
    public class NewService : INewService
    {

        private readonly IEmailService _emailService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly INotyfService _notyf;
        private readonly IRequestService _requestService;
        private IImageService _imageService;
        private NewsContext _newsContext;
        public NewService(NewsContext newsContext,IImageService imageService,INotyfService
         notyfService,IRequestService requestService,IHttpContextAccessor httpContextAccessor,IEmailService emailService)
        {
            _emailService = emailService;
            _httpContextAccessor=httpContextAccessor;
            _requestService = requestService;
            _notyf = notyfService;
            _imageService = imageService;
            _newsContext = newsContext;
        }
        public async Task<GeneralResponse<int>> CreateNew(New entity)
        {
            var response = new GeneralResponse<int>();

            try
            {
                await _newsContext.AddAsync(entity);
                var result= await _newsContext.SaveChangesAsync();

                var resultRequest= await _requestService.CreateRequest(entity.NewId,"WaitForPublish");
                if(!resultRequest.Success){
                   
                    response.Success = false;
                    response.Message = resultRequest.Message;
                    response.Errors = resultRequest.Errors;
                    return response;
                }
                response.Success = true;
                response.Message = "Haber Oluşturuldu";
                response.Data = result;
            }
            catch (System.Exception ex)
            {
                response.Errors.Add(ex.Message);
                response.Success = false;
                response.Data = 0;
                response.Message = "Haber Oluşturulamadı";
            }
            
           return response;
        }

        public async Task<GeneralResponse<int>> DeleteNew(int NewId)
        {
            var response = new GeneralResponse<int>();

            try
            {
                var New = await GetNews().Include(x=>x.Comments).Include(x=>x.Reactions).Where(x=>x.NewId==NewId).FirstOrDefaultAsync();
                if(New == null){
                    response.Message="Haber Bulunamadı";
                    response.Success =false;
                    return response;
                }
                if(!_imageService.DeleteOldPhoto(New.Image))
                    _notyf.Error("Görsel silinemedi");
                _newsContext.Remove(New);
                var resultDelete= await _newsContext.SaveChangesAsync();
                response.Success = true;
                response.Message = "Haber Başarılı Şekilde Silindi";
                response.Data = resultDelete;
            }
            catch (System.Exception ex)
            {
                response.Success=false;
                response.Message = "Haber silinemedi";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public IQueryable<New> GetNews()
        {
            var newsquery= _newsContext.News.AsQueryable();
            return newsquery;
        }
        public async Task<New> GetNewForDetail(int Id){
            return await _newsContext.News.Include(x=>x.User).ThenInclude(x=>x.User).Include(x=>x.Category).Include(x=>x.Comments).ThenInclude(x=>x.Replies).ThenInclude(x=>x.User).Where(x=>x.NewId==Id).FirstOrDefaultAsync();
        }
        public async Task<New> GetNewForEdit(int Id){
            return await _newsContext.News.Where(x=>x.NewId == Id).Include(x=>x.Category).FirstOrDefaultAsync();
        }

        public async Task<List<New>> GetNewForCategoryDetail(int Id){
            return await _newsContext.News.Where(x=>x.CategoryId==Id).ToListAsync();
        }

        public async Task<List<New>> GetAllNews(){
            return await _newsContext.News.Where(x=>x.IsConfirmed).OrderByDescending(x=>x.PublishedTime).ToListAsync();
        } 
        public async Task<List<New>> GetUserNews(int Id){
            
            return await _newsContext.News.Where(x=>x.UserId==Id).Include(x=>x.User).ToListAsync();
        }
        public async Task<GeneralResponse<int>> UpdateNew(EditNewDTO entity)
        {
            var response = new GeneralResponse<int>();
            var New = await GetNews().Where(x=>x.NewId == entity.NewId).FirstOrDefaultAsync();
            if(New == null){
                response.Success = false;
                response.Message = "Haber Bulunamadı";
                response.Data= 0;
               
                return response;
            }
            if(entity.ImageFile != null){
                var image = await _imageService.UploadImageAsync(entity.ImageFile);
                if(!_imageService.DeleteOldPhoto(New.Image)){
                    response.Success = false;
                    response.Message = "Haberin Eski Görseli Silinemedi";
                    response.Data = 0;
                    
                    return response;
                }
                New.Image = image;
            }
            New.Tittle = entity.Tittle;
            New.CategoryId = entity.CategoryId;
            New.Context = entity.Context;
            New.IsConfirmed = false;

            try
            {
                response.Data = await _newsContext.SaveChangesAsync();
                response.Success = true;
                response.Message = "Haber Güncellendi";
               
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Haber güncellenirken bir hata oluştu.";
                response.Errors.Add(ex.Message);
                
            }

            var requestResult = await _requestService.CreateRequest(New.NewId, "WaitForUpdate");
            
            if (!requestResult.Success)
            {
                response.Success = false;
                response.Message = requestResult.Message;
                response.Errors.AddRange(requestResult.Errors);
                
            }
            return response;
        }

        public async Task<GeneralResponse<int>> PublishNew(int NewId){
            var response = new GeneralResponse<int>();
            var New = await GetNews().Where(x=>x.NewId==NewId).FirstOrDefaultAsync();
            if(New==null){
                response.Success = false;
                response.Message = "Haber Bulunamadı";
                return response;
            }
            try
            {
                New.IsActive = true;
                var resultSave = await _newsContext.SaveChangesAsync();
                response.Success = true;
                response.Message = "Haber yayınlandı";
                response.Data = resultSave;
                
            }
            catch (System.Exception ex)
            {
                response.Success = false;
                response.Message = "Haber Yayınlanamdı";
                response.Errors.Add(ex.Message);
            }
            return response;
        }

        public async Task<GeneralResponse<int>> UnpublishNew(int NewId){
            var response = new GeneralResponse<int>();
            var New = await GetNews().Where(x=>x.NewId==NewId).FirstOrDefaultAsync();
            if(New==null){
                response.Success = false;
                response.Message = "Haber bulunamadı";
            }
            try
            {
                New.IsActive = false;
                var resultSave = await _newsContext.SaveChangesAsync();
                response.Success = true;
                response.Message = "Haber yayından kaldırıldı";
                response.Data = resultSave;
              
            }
            catch (System.Exception ex)
            {
                response.Success = true;
                response.Message = "Haber Yayından kaldırılamadı";
                response.Errors.Add(ex.Message);
                   
            }
            return response;
        }

        public async Task<List<New>> GetFilteredNews(string filter)
        {
            return await _newsContext.News.Where(x=>x.Context.Contains(filter)).ToListAsync();
        }

        public async Task<List<New>> GetConfirmedNews()
        {
            return await _newsContext.News.Where(x=>x.IsConfirmed == true).ToListAsync();
        }

        public async Task<GetRandomNewDTO> GetRandomNews()
        {
            var randomNews = await _newsContext.News.Where(x=>x.IsConfirmed==true)
                .OrderBy(e => Guid.NewGuid()) // Rastgele sıralama
                .Take(10) // İlk 10 satırı seç
                .ToListAsync();

            // İlk 5 haber ve ikinci 5 haber olarak ayırma
            var firstFive = randomNews.Take(5).ToList();
            var secondFive = randomNews.Skip(5).Take(5).ToList();

            // DTO'yu oluşturma ve haberleri gruplara ayırma
            return new GetRandomNewDTO
            {
                first = firstFive,
                second = secondFive
            };
        }

        public async Task<RandomNewDTO> GetRandomNew()
        {
            var randomNews = await _newsContext.News
                .OrderBy(e => Guid.NewGuid()) // Rastgele sıralama
                .Take(5) // İlk 10 satırı seç
                .ToListAsync();
            return new RandomNewDTO{
                RandomNews = randomNews
            };
        }

        public async Task<GeneralResponse<Comment>> CreateComment(CreateCommentDTO createCommentDTO)
        {
            var response = new GeneralResponse<Comment>();
            var comment = new Comment{
                Context = createCommentDTO.Context,
                NewId=createCommentDTO.NewId,
                PublishedDate = createCommentDTO.PublishedDate,
                UserId = createCommentDTO.UserId,
            };
            try
            {
                await _newsContext.Comments.AddAsync(comment);
                await _newsContext.SaveChangesAsync();
                response.Success = true;
                response.Message = "Yorum Başarı ile Eklendi";
                response.Data = comment;
            }
            catch (System.Exception ex)
            {
                response.Errors.Add(ex.Message);
                response.Success =false;
                response.Message="Yorum Kaydedilemedi"; 
            }
            return response;
        }

        public async Task<List<Comment>> GetCommentsForNew(int NewId)
        {
            return await _newsContext.Comments.Where(c=>c.NewId == NewId).Include(u=>u.User).Include(x=>x.Replies).OrderByDescending(x=>x.PublishedDate).ToListAsync();
        }

        public async Task<GeneralResponse<KeepReactionCount>> AddReactionToComment(int CommentId, bool reactionType,string comtype)
        {
            var UserId = int.Parse( _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
            var response = new GeneralResponse<KeepReactionCount>();
            if(comtype == "comment"){

                var comment = await _newsContext.Comments.FindAsync(CommentId);

                if (comment == null)
                {
                    response.Success = false;
                    response.Message = "Yorum Bulunamadı";
                    return response;
                }
    // Kullanıcının mevcut reaksiyonunu al
                var existingReaction = await _newsContext.CommentReactions
                    .Where(cr => cr.CommentId == CommentId && cr.UserId == UserId)
                    .FirstOrDefaultAsync();

                if (existingReaction != null)
                {
                    // Kullanıcının mevcut tepkisini güncelle
                    if (existingReaction.IsLike == reactionType)
                    {
                        response.Success = false;
                        response.Message = "Bu yoruma zaten bu tür bir tepki verdiniz.";
                        return response;
                    }
                    else
                    {
                        // Tepki türünü değiştirme
                        if (reactionType)
                        {
                            comment.LikeCount++;
                            comment.DisslikeCount--;
                        }
                        else
                        {
                            comment.DisslikeCount++;
                            comment.LikeCount--;
                        }

                        existingReaction.IsLike = reactionType;
                        _newsContext.CommentReactions.Update(existingReaction);
                    }
                    }
                    else
                    {
                        // Yeni tepki ekleme
                        var newReaction = new CommentReaction
                        {
                            CommentId = CommentId,
                            UserId = UserId,
                            IsLike = reactionType
                        };

                        _newsContext.CommentReactions.Add(newReaction);

                        if (reactionType)
                        {
                            comment.LikeCount++;
                        }
                        else
                        {
                            comment.DisslikeCount++;
                        }
                    }

                    await _newsContext.SaveChangesAsync();
                    response.Success = true;
                    response.Message = "Reaksiyon başarıyla eklendi";
                    response.Data = new KeepReactionCount
                    {
                        LikeCount = comment.LikeCount,
                        DisslikeCount = comment.DisslikeCount
                    };
            }
            else{
                var reply = await _newsContext.CommentReplies.FindAsync(CommentId);

                if (reply == null)
                {
                    response.Success = false;
                    response.Message = "Yorum Bulunamadı";
                    return response;
                }
                // Kullanıcının mevcut reaksiyonunu al
                var existingReaction = await _newsContext.CommentReactions
                    .Where(cr => cr.CommentId == CommentId && cr.UserId == UserId)
                    .FirstOrDefaultAsync();

                if (existingReaction != null)
                {
                    // Kullanıcının mevcut tepkisini güncelle
                    if (existingReaction.IsLike == reactionType)
                    {
                        response.Success = false;
                        response.Message = "Bu yoruma zaten bu tür bir tepki verdiniz.";
                        return response;
                    }
                    else
                    {
                        // Tepki türünü değiştirme
                        if (reactionType)
                        {
                            reply.LikeCount++;
                            reply.DisslikeCount--;
                        }
                        else
                        {
                            reply.DisslikeCount++;
                            reply.LikeCount--;
                        }

                        existingReaction.IsLike = reactionType;
                        _newsContext.CommentReactions.Update(existingReaction);
                    }
                    }
                    else
                    {
                        // Yeni tepki ekleme
                        var newReaction = new CommentReaction
                        {
                            CommentId = CommentId,
                            UserId = UserId,
                            IsLike = reactionType
                        };

                        _newsContext.CommentReactions.Add(newReaction);

                        if (reactionType)
                        {
                            reply.LikeCount++;
                        }
                        else
                        {
                            reply.DisslikeCount++;
                        }
                    }

                    await _newsContext.SaveChangesAsync();
                    response.Success = true;
                    response.Message = "Reaksiyon başarıyla eklendi";
                    response.Data = new KeepReactionCount
                    {
                        LikeCount = reply.LikeCount,
                        DisslikeCount = reply.DisslikeCount
                    };
            }
            return response;    
            }
        

        public async Task<GeneralResponse<CommentReply>> AddReplyToComment(AddReplyDTO replyDTO)
        {
            var response = new GeneralResponse<CommentReply>();
            try
            {
                var reply = new CommentReply{
                    PublishedDate=DateTime.UtcNow.AddHours(3),
                    CommentId=replyDTO.CommentId,
                    UserId = replyDTO.UserId,
                    Text = replyDTO.Context,
                };
                await _newsContext.CommentReplies.AddAsync(reply);
                await _newsContext.SaveChangesAsync();
                 
                var notify = new Notify{
                    Context=$" <b>'{replyDTO.UserName}'</b> adlı kullanıcı '<i>{replyDTO.Comment}</i>' yorumunuza '<i> {replyDTO.Context}</i>' yanıtını verdi yoruma gitmek için <a href='http://localhost:5254/New/Detail/{replyDTO.NewId}'>tıklayın.</a>",
                    UserId = replyDTO.CommentUserId,
                    ViewId=replyDTO.NewId,
                    Query = $"http://localhost:5254/New/Detail/{replyDTO.NewId}"
                };
                //await _emailService.SendEmailAsync(reply.User.Email,"Yorum Yanıtı",notify.Context);
                await _newsContext.Notifies.AddAsync(notify);
                await _newsContext.SaveChangesAsync();
              
                response.Success =true;
                response.Message = "Yanıt Başarı İle Eklendi";
                response.Data = reply;
            }
            catch (System.Exception ex)
            {
                response.Errors.Add(ex.Message);
                response.Success = false;
                response.Message = "Yanıt Eklenemedi";                
            }
            return response;
        }

        public async Task<List<Comment>> GetComments()
        {
            return await _newsContext.Comments.Include(x=>x.User).Include(x=>x.Replies).ThenInclude(x=>x.User).ToListAsync();
        }

        public async Task<GeneralResponse<int>> DeleteComment(int CommentId, string type,int? WarningId)
        {
            var response = new GeneralResponse<int>();
            try
            {             
                if(type=="reply"){
                    var reply = await _newsContext.CommentReplies.Where(x=>x.ReplyId == CommentId).Include(x=>x.Warnings).FirstOrDefaultAsync() ?? new CommentReply();
                    
                    _newsContext.CommentReplies.Remove(reply);
                    response.Data = reply.ReplyId;
                }
                else{
                    var comment = await _newsContext.Comments.Where(x=>x.CommentId == CommentId).Include(x=>x.Warnings).Include(x=>x.Replies).FirstOrDefaultAsync() ?? new Comment();
                    _newsContext.Comments.Remove(comment);
                    if(comment.Warnings!=null)
                        _newsContext.WarningComments.RemoveRange(comment.Warnings);
                    response.Data = comment.CommentId;
                }
            
                var reactions = await _newsContext.CommentReactions.Where(x=>x.CommentId == CommentId).ToListAsync();
                if(reactions!=null)
                    _newsContext.CommentReactions.RemoveRange(reactions);
               
                await _newsContext.SaveChangesAsync();
                response.Success = true;
                response.Message = "Başarıyla Silindi";
                
                
            }
            catch (System.Exception ex)
            {
                response.Errors.Add(ex.Message);
                response.Success=false;
                response.Message = "Silme Başarısız";
            }
            return response;
        }

        public async Task<GeneralResponse<New>> AddReaction(AddReactionDTO addReactionDTO){
            var response = new GeneralResponse<New>();

            try
            {
                var NewItem = await _newsContext.News.FirstOrDefaultAsync(x=>x.NewId == addReactionDTO.NewId);

                if(_newsContext.NewReactions.Any(x=>x.NewId==addReactionDTO.NewId && x.UserId==addReactionDTO.UserId && x.IsLike == addReactionDTO.IsLike)){
                    response.Success = false;
                    response.Message = "Zaten Bu Habere Aynı tepkiyi vermişssiniz";
                    return response;
                }
                    // await _newsContext.NewReactions.AddAsync(reaction);
                    //daha önce bu habere başka tepki verilmiş ise değişsin
                else if(_newsContext.NewReactions.Any(x=>x.NewId == addReactionDTO.NewId && x.UserId == addReactionDTO.UserId && x.IsLike != addReactionDTO.IsLike)){
                    var reaction = await _newsContext.NewReactions.FirstOrDefaultAsync(x=>x.NewId==addReactionDTO.NewId&&x.UserId==addReactionDTO.UserId);

                    if(reaction.IsLike){
                        NewItem.DisslikeCount++;
                        NewItem.LikeCount--;
                        reaction.IsLike = false;
                    }
                    
                    else{
                        NewItem.LikeCount++;
                        NewItem.DisslikeCount--;
                        reaction.IsLike = true;
                    }
                    await _newsContext.SaveChangesAsync();
                    response.Success = true;
                    response.Message ="Tepki Değiştirildi";
                    response.Data = NewItem;
                }  
                //YENİ TEPKİ EKLEME
                else{
                    var reaction = new NewReaction{
                        IsLike = addReactionDTO.IsLike,
                        NewId = addReactionDTO.NewId,
                        UserId = addReactionDTO.UserId,
                    };
                    if(reaction.IsLike)
                        NewItem.LikeCount++;
                    else
                        NewItem.DisslikeCount++;
                    _newsContext.NewReactions.Add(reaction);
                    await _newsContext.SaveChangesAsync();
                    response.Message="Tepki Eklendi";
                    response.Success = true;
                    response.Data = NewItem;
                }
                
            }
            catch (System.Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Errors.Add(ex.Message);
            }
            return response;
        }    
    }
}