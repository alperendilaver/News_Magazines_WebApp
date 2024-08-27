using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;
using News.Data.Data.Models;
using News.Data.DTOs.ChannelDTOs;

namespace News.Data.Services.AbstractServices
{
    public interface IChannelService
    {
        public Task<GeneralResponse<int>> CreateChannel(CreateChannelDTO createChannelDTO);
        public Task<GeneralResponse<int>> DeleteChannel(int channelId);
        public Task<Data.Models.Channel> GetChannel(int? channelId);

        public Task<List<User>> GetMembers(int ChannelId);    
        public Task<List<int>> GetUserChannelsId(int userId);
        public Task<List<int>> GetUserRequestChannelId (int userId);
        public Task<List<Data.Models.Channel>> GetChannels();

        public Task<Data.Models.Channel> GetChannelByAuthorId(int? userId);
        public Task<int> GetMemCount(int channelId);

        public Task<GeneralResponse<Post>> CreatePost(CreatePostDTO createPostDTO);

        public Task<GeneralResponse<Post>> AddReactionToPost(AddReactionPostDTO addReactionToPostDTO);

        public Task<GeneralResponse<int>> DeletePost(int postId);

        public  Task<GeneralResponse<int>> RemoveMember(int userId,int channelId);
    }
}