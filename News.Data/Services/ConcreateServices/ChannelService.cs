using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using News.Data.Data.Context;
using News.Data.Data.Models;
using News.Data.DTOs.ChannelDTOs;
using News.Data.Services.AbstractServices;

namespace News.Data.Services.ConcreateServices
{
    public class ChannelService : IChannelService
    {
        private readonly IHashService _hashService;
        private NewsContext _newsContext;
        public ChannelService(NewsContext newsContext,IHashService hashService)
        {
            _hashService=hashService;
            _newsContext = newsContext;
        }

        public async Task<GeneralResponse<Post>> AddReactionToPost(AddReactionPostDTO addReactionToPostDTO)
        {
            var response = new GeneralResponse<Post>();
            try{
            var post = await _newsContext.ChannelPosts.FirstOrDefaultAsync(x=>x.PostId==addReactionToPostDTO.PostId);

            if(_newsContext.PostReactions.Any(x=>x.UserId ==addReactionToPostDTO.UserId&&x.PostId==addReactionToPostDTO.PostId && x.IsLike==addReactionToPostDTO.IsLike)){
                response.Success = false;
                    response.Message = "Zaten Bu gönderiye Aynı tepkiyi vermişssiniz";
                    return response;  
            }
            else if(_newsContext.PostReactions.Any(x=>x.PostId==addReactionToPostDTO.PostId&&x.UserId==addReactionToPostDTO.UserId&&x.IsLike!=addReactionToPostDTO.IsLike)){
                var reaction = await _newsContext.PostReactions.FirstOrDefaultAsync(x=>x.PostId==addReactionToPostDTO.PostId&&x.UserId==addReactionToPostDTO.UserId);
                if (reaction.IsLike)
                {
                    post.DisslikeCount++;
                    post.LikeCount--;
                    reaction.IsLike=false;
                }
                else
                {
                    post.DisslikeCount--;
                    post.LikeCount++;
                    reaction.IsLike=true;

                }
                await _newsContext.SaveChangesAsync();
                response.Success=true;
                response.Message = "Tepki Değiştirildi";
                response.Data=post;
            }
            else
            {
                var reaction = new PostReaction{
                    PostId=addReactionToPostDTO.PostId,
                    UserId=addReactionToPostDTO.UserId,
                    IsLike=addReactionToPostDTO.IsLike
                };
                if (reaction.IsLike)
                    post.LikeCount++;
                else
                    post.DisslikeCount++;
                _newsContext.PostReactions.Add(reaction);
                await _newsContext.SaveChangesAsync();
                response.Message="Tepki Eklendi";
                    response.Success = true;
                    response.Data = post;       
            }
            }
            catch(System.Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Errors.Add(ex.Message);
            }
            return response;
        }

        public async Task< GeneralResponse<int>> CreateChannel(CreateChannelDTO createChannelDTO)
        {
            var response = new GeneralResponse<int>();
            try
            {
                var channel = new Data.Models.Channel{AuthorId=createChannelDTO.AuthorId,ChannelName=createChannelDTO.ChannelName}; 
                await _newsContext.Channels.AddAsync(channel);
                await _newsContext.SaveChangesAsync();
                var author = await _newsContext.Authors.Include(x=>x.User).FirstOrDefaultAsync(x=>x.UserId==createChannelDTO.AuthorId);
                channel.Members.Add(author.User);
                author.ChannelId = channel.ChannelId;
                await _newsContext.SaveChangesAsync();  
                response.Message ="Kanal başarı ile oluşturuldu";
                response.Success = true;
                response.Data = channel.ChannelId;
            }
            catch (System.Exception ex)
            {
                response.Errors.Add( ex.Message);
                response.Success =false;
                response.Message = "Kanal oluşturulamadı";
            }
            return response;
        }

        public async Task<GeneralResponse<Post>> CreatePost(CreatePostDTO createPostDTO)
        {
            var channel = await _newsContext.Channels.FindAsync(createPostDTO.ChannelId);
            var author = await _newsContext.Authors.FindAsync(createPostDTO.AuthorId);
            var response = new GeneralResponse<Post>();
            try
            {
                var PostId=_hashService.HashingForPostId($"{createPostDTO.AuthorId}{createPostDTO.Context}{createPostDTO.ChannelId}{DateTime.UtcNow.AddHours(3).ToString()}");
                var post = new Post{
                    PostId=PostId,
                    AuthorId = createPostDTO.AuthorId,
                    Author = author,
                    ChannelId = createPostDTO.ChannelId,
                    Context = createPostDTO.Context,
                    PublishedTime=DateTime.UtcNow.AddHours(3),
                    Channel = channel
                };
                await _newsContext.ChannelPosts.AddAsync(post);
                await _newsContext.SaveChangesAsync();
                response.Success=true;
                response.Message = "Gönderi Paylaşıldı";
                response.Data = post;
            
            }
            catch (System.Exception ex)
            {
                response.Errors.Add(ex.Message);
                response.Success = false;
                response.Message="Gönderi Paylaşılamadı";
            }
            return response;
        }

        public async Task<GeneralResponse<int>> DeleteChannel(int channelId)
        {
            var response = new GeneralResponse<int>();
            try
            {
                var channel = await _newsContext.Channels.Include(x=>x.Posts).Include(x=>x.Members).FirstOrDefaultAsync(x=>x.ChannelId==channelId);
                channel.Members.Clear();
                channel.Posts.Clear();
                response.Data=channel.AuthorId;
                _newsContext.Channels.Remove(channel);
                await _newsContext.SaveChangesAsync(); 
                response.Success=true;
                response.Message="Kanal Silindi";

            }
            catch (System.Exception ex)
            {
                response.Message="Kanal Silinemedi";
                response.Errors.Add(ex.Message);
                response.Success = false;
            }
            return response;
        }

        public async Task<GeneralResponse<int>> DeletePost(int postId)
        {
            var response = new GeneralResponse<int>();
            try
            {
                var post =await _newsContext.ChannelPosts.Where(x=>x.PostId==postId).Include(x=>x.Reactions).FirstOrDefaultAsync();
                _newsContext.ChannelPosts.Remove(post);
                if(post.Reactions!=null)
                    _newsContext.PostReactions.RemoveRange(post.Reactions);
                await _newsContext.SaveChangesAsync();
                response.Success = true;
                response.Message = "Gönderi Silindi";
                response.Data = post.PostId;
            }
            catch (System.Exception ex)
            {
                response.Success = false;
                response.Message = "Gönderi Silinmedi";
                response.Errors.Add(ex.Message);
            }
            return response;
        }

        public async Task<Data.Models.Channel> GetChannel(int? channelId)
        {
            return await _newsContext.Channels.Where(x=>x.ChannelId == channelId).Include(x=>x.Author).Include(x=>x.Posts).Include(x=>x.Members).FirstOrDefaultAsync()??new Data.Models.Channel();
        }

        public async Task<Data.Models.Channel> GetChannelByAuthorId(int? userId)
        {
            return await _newsContext.Channels.Where(x=>x.AuthorId == userId).Include(x=>x.Author).Include(x=>x.Posts).Include(x=>x.Members).FirstOrDefaultAsync()??new Data.Models.Channel();

        }

        public async Task<List<Data.Models.Channel>> GetChannels()
        {
            return await _newsContext.Channels.Include(x=>x.Author).ThenInclude(x=>x.User).Include(x=>x.Members).ToListAsync();
        }

        public async Task<List<User>> GetMembers(int ChannelId)
        {
            return await _newsContext.Channels.Where(x=>x.ChannelId==ChannelId).Include(x=>x.Members).SelectMany(x=>x.Members).ToListAsync();
        }

        public async Task<int> GetMemCount(int ChannelId)
        {
            return  await _newsContext.Channels.Include(x=>x.Members).Where(x=>x.ChannelId==ChannelId).Select(x=>x.Members).CountAsync();
        }

        public async Task<List<int>> GetUserChannelsId(int userId)
        {
            var userChannelIds =await _newsContext.Users.Where(x=>x.UserId==userId).Include(x=>x.Channels).SelectMany(x=>x.Channels.Select(x=>x.ChannelId)).ToListAsync();
            return userChannelIds!=null ?  userChannelIds: new List<int>();
        }

        public async Task<List<int>> GetUserRequestChannelId(int userId)
        {
            return await _newsContext.ChannelRequests.Where(x=>x.UserId== userId).Select(x=>x.ChannelId).ToListAsync();
        }

        public async Task<GeneralResponse<int>> RemoveMember(int userId, int channelId)
        {
            var response = new GeneralResponse<int>();

            try
            {
                var query = _newsContext.Channels.Include(d=>d.Members).Where(x=>x.ChannelId==channelId).AsQueryable();
                var member = await query.SelectMany(x=>x.Members).Where(x=>x.UserId==userId).Include(x=>x.PostReactions).FirstOrDefaultAsync();
                var channel = await query.FirstOrDefaultAsync();
                channel.Members.Remove(member);
                var reactions = await _newsContext.PostReactions.Include(x=>x.Post).Where(x=>x.UserId == userId && x.Post.ChannelId ==channelId).ToListAsync();
                _newsContext.PostReactions.RemoveRange(reactions);
                await _newsContext.SaveChangesAsync();
                response.Success = true;
                response.Message = "Üye Çıkarımı Başarılı";
            }
            catch (System.Exception ex)
            {
                response.Message = "Üye Çıkarılamadı";
                response.Success = false;
                response.Errors.Add(ex.Message);
            }
            return response;
        }
    }
}