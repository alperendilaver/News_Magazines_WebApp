using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using News.Data.Data.Models;
using News.Data.DTOs.UserDTOs;


namespace News.Data.Services.AbstractServices
{
    public interface IUserService
    {
        public Task<User> GetUser(int Id);
        
        public Task<User> GetUser(string Email);

        public IQueryable<User> GetUsers();

        public Task<User> GetUserWithAuthor(int Id);

        public Task<List<User>> GetUsersByRole(string Role);

        public Task<List<User>> GetAllUsers();
        public Task<GeneralResponse<int>> Register(UserRegisterDTO registerDto);

        public Task<User> Login(UserLoginDTO loginDTO);

        public Task SendResetPassword(User User);

        public Task<GeneralResponse<int>> ResetPassword(int UserId,string password,string token);

        public Task<Author> GetAuthor(int UserId);

        public Task<List<Author>> GetAuthors();

        public Task<GeneralResponse<int>> Delete(int UserId);

        public Task<List<Notify>> GetNotifies(int UserId);

        public Task<GeneralResponse<int>> MarkAsRead(int NotifyId);

        public Task<GeneralResponse<int>> DeleteNotify(int NotifyId);
    }
}