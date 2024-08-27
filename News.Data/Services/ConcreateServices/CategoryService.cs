using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using News.Data.Data.Context;
using News.Data.Data.Models;
using News.Data.Services.AbstractServices;

namespace News.Data.Services.ConcreateServices
{
    public class CategoryService : ICategoryService
    {
        private readonly NewsContext _newsContext;
        public CategoryService(NewsContext newsContext)
        {
            _newsContext = newsContext;
        }

        public async Task<List<Category>> GetAllCategories()
        {
          return await _newsContext.Categories.ToListAsync();
        }

        public async Task<List<Category>> GetAuthorCategories(int Id,string role)
        {
            if(role == "Editor")
                return await _newsContext.Categories.ToListAsync();
            var query= _newsContext.Authors.Where(i=>i.UserId == Id).Include(x=>x.Category).AsQueryable();
            return await query.SelectMany(x=>x.Category).ToListAsync() ?? new List<Category>(); 
        }
    }
}