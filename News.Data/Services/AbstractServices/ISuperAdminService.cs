using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using News.Data.Data.Models;

namespace News.Data.Services.AbstractServices
{
    public interface ISuperAdminService
    {
        public Task<GeneralResponse<int>> AddEditorOrAuthor(int UserId,int Role);
        public Task<GeneralResponse<int>> AddReaderFromEditor(int UserId);
        public Task<GeneralResponse<int>> AddEditorFromAuthorOrReverse(int UserId,int RoleId);
        public Task<GeneralResponse<int>> AddCategory(string CategoryName);
        public Task<GeneralResponse<int>> DeleteCategory(int CatId);


    }
}