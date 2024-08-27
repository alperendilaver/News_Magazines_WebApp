using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using News.Data.Data.Context;
using News.Data.Data.Models;
using News.Data.Services.AbstractServices;
using News.Data.DTOs.UserDTOs;
//exception middleware araştırması
//general response kullanılabilir 
//eski şifre kontrolu
namespace News.Data.Services.ConcreateServices
{
    public class UserService : IUserService
    {
        private NewsContext _newsContext;
        private IEmailService _emailService;
        private HashService _passwordService;
        private readonly IUrlHelper _urlHelper;
        private ITokenService _tokenService;
        private readonly INotyfService _notyf;
       
        public UserService(NewsContext newsContext,IEmailService emailService,IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionContextAccessor,ITokenService tokenService,INotyfService notyf)
        { 
            _notyf = notyf;
            _tokenService=tokenService;
            _urlHelper=urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);;
            _emailService = emailService;
            _passwordService = HashService.Instance;
            _newsContext=newsContext;  
        }
        public async Task<User> Login(UserLoginDTO loginDTO)
        {
            var user = await _newsContext.Users.Where(i=>i.Email == loginDTO.Mail).Include(x=>x.Role).FirstOrDefaultAsync();
            if (user == null){
                _notyf.Error("Kullanıcı Bulunamadı");
                return new User();
            }
            if(user.IsEmailConfirmed ==false){
                _notyf.Warning("Mail hesabınıza gelen bağlantı ile hesabınızı onaylamanız gerekmektedir");
                return new User();
            }
            var PasswordForTryLogin = _passwordService.Hashing(user.Email+loginDTO.Password);
            var result = _passwordService.Verify(PasswordForTryLogin,user.HashedPassword);
            if(result == true){
                _notyf.Success("Giriş Başarılı");
                return user;
            }
            _notyf.Error("Giriş Başarısız");
            return new User(); 
        }

        public async Task<GeneralResponse<int>> Register(UserRegisterDTO registerDTO)
        {
            var response = new GeneralResponse<int>();

            try
            {
                var hashedPassword = _passwordService.Hashing(registerDTO.Mail + registerDTO.Password);
                var user = new User
                {
                    Email = registerDTO.Mail,
                    FirstName = registerDTO.FirstName,
                    LastName = registerDTO.SurName,
                    NormalizedEmail = registerDTO.Mail.ToUpper(),
                    IsEmailConfirmed = false,
                    PhoneNumber = registerDTO.PhoneNumber,
                    RoleId = 1,
                    HashedPassword = hashedPassword
                };

                await _newsContext.AddAsync(user);
                var resultUserAdd = await _newsContext.SaveChangesAsync();
                await GenerateTokenAndSendMail(user, "ConfirmEmail", "Hesap Onayı", "confirm");

                response.Success = true;
                response.Message = "Kullanıcı başarıyla kaydedildi.";
                response.Data = resultUserAdd;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Kullanıcı kaydedilemedi.";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public async Task SendResetPassword(User User)
        {
            await GenerateTokenAndSendMail(User,"ResetPassword","Parola Sıfırlama","reset");
        }

        

        private async Task GenerateTokenAndSendMail(User user,string ActionName,string title,string type){
            var token = _passwordService.Hashing(user.UserId.ToString(),type);
            var url = _urlHelper.Action(ActionName,"Users",new{user.UserId,token});
            await _emailService.SendEmailAsync(user.Email,title,$"Lütfen {title} için <a href='http://localhost:5254{url}'>tıklayın.</a>");
            await _tokenService.SaveTokentoDatabase(user.UserId,token,type,DateTime.UtcNow.AddHours(24));
        }

        public async Task<User> GetUser(int Id)
        {
            var user = await _newsContext.Users.Where(x=>x.UserId==Id).Include(x=>x.Author).ThenInclude(x=>x.News).Include(x=>x.Author).ThenInclude(x=>x.Channel).FirstOrDefaultAsync();
            return user ?? new User();
        }

        public async Task<User> GetUser(string email)
        {
            var user = await _newsContext.Users.FirstOrDefaultAsync(x=>x.Email==email);
            return user ?? new User();
        }
        

        public async Task<GeneralResponse<int>> ResetPassword(int UserId, string password,string token)
        {
            var response = new GeneralResponse<int>();
            try
            {
                var user = await _newsContext.Users.FirstOrDefaultAsync(x=>x.UserId==UserId);
                if (user == null)
                    {
                        response.Success = false;
                        response.Message="Kullanıcı Bulunamadı";
                        return response;
                    }

                var HashedNewPassword =  _passwordService.Hashing(user.Email+password);
                user.HashedPassword = HashedNewPassword;
                await _tokenService.VerifyToken(UserId,token);
                var result = await _newsContext.SaveChangesAsync(); 
                response.Success = true;
                response.Message = "Parola başarılı şekilde güncellendi";
                response.Data = result;
            }
            catch (System.Exception ex)
            {
                response.Message = "Parola güncellenemedi";
                response.Success = false;
                response.Errors.Add(ex.Message);    
            }
            return response;
        }

        public IQueryable<User> GetUsers()
        {
            return  _newsContext.Users.Where(x=>x.RoleId!=3).Include(x=>x.Role).AsQueryable();
        }

        public async Task<Author> GetAuthor(int UserId)
        {
            return await _newsContext.Authors.Where(x=>x.UserId==UserId).Include(x=>x.News).FirstOrDefaultAsync() ?? new Author();
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _newsContext.Users.ToListAsync();
        }

        public async Task<List<User>> GetUsersByRole(string Role)
        {
            return await _newsContext.Users.Where(x=>x.Role.RoleName==Role).Include(x=>x.Role).ToListAsync() ?? new List<User>();
        }

        public async Task<User> GetUserWithAuthor(int Id)
        {
            return await _newsContext.Users.Where(x=>x.UserId == Id).Include(x=>x.Role).Include(x=>x.Author).ThenInclude(x=>x.News).ThenInclude(x=>x.Category).FirstOrDefaultAsync() ?? new User();
    }

        public async Task<List<Author>> GetAuthors()
        {
            return await _newsContext.Authors.Where(x=>x.Unvan=="Author").Include(x=>x.User).ToListAsync();
        }

        public async Task<GeneralResponse<int>> Delete(int UserId)
        {
            var response = new GeneralResponse<int>();
            try
            {
                var user = await _newsContext.Users.Include(x=>x.Author).ThenInclude(x=>x.News).FirstOrDefaultAsync(x=>x.UserId==UserId);
                _newsContext.Remove(user);
                var resultRemove=await _newsContext.SaveChangesAsync();
                response.Success = true;
                response.Message = "Kullanıcı Başarı ile silindi";
                response.Data = resultRemove;
            }
            catch (System.Exception ex)
            {
                response.Success = false;
                response.Message = "Kullanıcı Silinemedi";
                response.Errors.Add(ex.Message);
            }
            return response;
        }

        public async Task<List<Notify>> GetNotifies(int UserId)
        {
            var Notifies = await _newsContext.Notifies.Where(x=>x.UserId == UserId).ToListAsync();
            return Notifies;
        }

        public async Task<GeneralResponse<int>> MarkAsRead(int NotifyId)
        {
            var response = new GeneralResponse<int>();
            try
            {
                var notify = await _newsContext.Notifies.Where(x=>x.NotifyId == NotifyId).FirstOrDefaultAsync();
                notify.IsSeen = true;
                await _newsContext.SaveChangesAsync();
                response.Success=true;
                response.Message = "Bildirim Okundu olarak işaretlendi";
                response.Data = notify.NotifyId;
            }
            catch (System.Exception ex)
            {
                response.Errors.Add(ex.Message);
                response.Success = false;
                response.Message="Bildirim okundu olarak işaretlenemedi";
                
            }
            return response;
        }

        public async Task<GeneralResponse<int>> DeleteNotify(int NotifyId)
        {
            var response = new GeneralResponse<int>();
            try
            {
                var notify = await _newsContext.Notifies.Where(x=>x.NotifyId==NotifyId).FirstOrDefaultAsync();
                _newsContext.Notifies.Remove(notify);
                await _newsContext.SaveChangesAsync();
                response.Success=true;
                response.Message="Bildirim Silindi";
                response.Data = notify.NotifyId;
            }
            catch (System.Exception ex)
            {
                response.Errors.Add(ex.Message);
                response.Success=false;
                response.Message="Bildirim silinemedi";

            }
            return response;
        }
    }
}