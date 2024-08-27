using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using News.Data.Data.Models;

namespace News.Data.Services.AbstractServices
{
    public interface ICategoryService
    {
        public Task<List<Category>> GetAllCategories();
    
        public Task<List<Category>> GetAuthorCategories(int Id,string role); 
    }

}