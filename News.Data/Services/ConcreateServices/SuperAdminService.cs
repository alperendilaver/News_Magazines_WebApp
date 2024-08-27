using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.EntityFrameworkCore;
using News.Data.Data.Context;
using News.Data.Data.Models;
using News.Data.Services.AbstractServices;

namespace News.Data.Services.ConcreateServices
{
    public class SuperAdminService : ISuperAdminService
    {

        private readonly NewsContext _newsContext;
        public SuperAdminService(NewsContext newsContext)
        {
            _newsContext = newsContext;
        }


        public async Task<GeneralResponse<int>> AddEditorOrAuthor(int UserId,int role)
        {
            var response = new GeneralResponse<int>();
            var user = await _newsContext.Users.FirstOrDefaultAsync(x => x.UserId == UserId);
            if (user == null){
                response.Success = false;
                response.Message="Kullanıcı Bulunamadı";
            }

            try
            {
                var author = new Author{User = user};
                if(role==4){
                user.RoleId = 4;
                author.Unvan="Editor";
                }
                else if(role==2){
                    user.RoleId = 2;
                    author.Unvan="Author";
                }
                _newsContext.Authors.Add(author);
                var resultSave=  await _newsContext.SaveChangesAsync();

                response.Success = true;
                response.Data = resultSave;
                response.Message =$"{UserId} Nolu kullanıcı {role} yapıldı";
            }
            catch (System.Exception ex)
            {
                response.Success=false;
                response.Message = "Rol değişimi yapılamadı";
                response.Errors.Add(ex.Message);
            }
            return response;
        }
        public async Task<GeneralResponse<int>> AddEditorFromAuthorOrReverse(int UserId,int RoleId)
        {
            var response = new GeneralResponse<int>();
            var user= await _newsContext.Users.Include(x=>x.Author).FirstOrDefaultAsync(x=>x.UserId == UserId);
            if (user == null){
                response.Success = false;
                response.Message = "Kullanıcı bulunamadı";
            }

            try
            {  
                if(RoleId==4){
                    user.RoleId = 4;
                    user.Author.Unvan = "Editor";
                }
                else if(RoleId == 2){
                    user.RoleId = 2;
                    user.Author.Unvan="Author";
                }
                var resultsave= await _newsContext.SaveChangesAsync();
                response.Message=$"Kullanıcı {user.Author.Unvan} Yapıldı";
                response.Success = true;
                response.Data=resultsave;
            }
            catch (System.Exception ex)
            {
                response.Errors.Add(ex.Message);
                response.Success=false;
                response.Message = "Kullanıcı Editör Yapılamadı";
            }
            return response;
        }
        public async Task<GeneralResponse<int>> AddReaderFromEditor(int UserId)
        {
            var response = new GeneralResponse<int>();
            var user = await _newsContext.Users.FirstOrDefaultAsync(x => x.UserId == UserId);
            if (user == null){
                response.Success = false;
                response.Message = "Kullanıcı bulunamadı";
            }

            try
            {
                user.RoleId = 1;
                var author = await _newsContext.Authors.Include(x=>x.News).FirstOrDefaultAsync(x =>x.UserId == UserId);
                foreach (var item in author.News)
                {
                    _newsContext.News.Remove(item);
                }
            
                _newsContext.Authors.Remove(author);
                var resultSave =  await _newsContext.SaveChangesAsync();

                response.Success=true;
                response.Message = $"{UserId} Nolu kullanıcı reader yapıldı";
                response.Data = resultSave;
            }
            catch (System.Exception ex)
            {
                response.Errors.Add(ex.Message);
                response.Success = false;
                response.Message = "Rol değişimi yapılamadı";
            }

            return response;
        }
        public async Task<GeneralResponse<int>> AddCategory(string CategoryName)
        {
            var response = new GeneralResponse<int>();
            if(_newsContext.Categories.Any(x=>x.CategoryName==CategoryName)){
                response.Success = false;
                response.Message = $"{CategoryName} zaten mevcut";
                return response;
            }
            try
            {
                await _newsContext.Categories.AddAsync(new Category{CategoryName = CategoryName});
                var resultSave =  await _newsContext.SaveChangesAsync();

                response.Success = true;
                response.Message = "Kategori Eklendi";
                response.Data = resultSave;
            }
            catch (System.Exception ex)
            {
                response.Success=false;
                response.Message="Kategori Eklenemedi";
                response.Errors.Add(ex.Message);            
            }
            return response;
        }
        public async Task<GeneralResponse<int>> DeleteCategory(int CatId)
        {
            var response= new GeneralResponse<int>();
            var entity = await _newsContext.Categories.FirstOrDefaultAsync(x => x.CategoryId==CatId);
            if (entity == null){
                response.Success=false;
                response.Message ="Kategori Bulunamadı";
                return response;
            }
            try
            {
                _newsContext.Remove(entity);
                var resultSave =  await _newsContext.SaveChangesAsync();
                response.Success = true;
                response.Message = "Kategori Silindi";
                response.Data = resultSave;
            }
            catch (System.Exception ex)
            {
                response.Success=false;
                response.Message = "Kategori silinemedi";
                response.Errors.Add($"{ex.Message}");
            }
            return response;
        }

       
    }
}