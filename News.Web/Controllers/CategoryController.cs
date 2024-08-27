using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using News.Data.Services.AbstractServices;

namespace News.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly INewService _newService;
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService,INewService newService)
        {
            _newService = newService;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _categoryService.GetAllCategories());
        }

        public async Task<IActionResult> Detail(int Id){
            var News = await _newService.GetNewForCategoryDetail(Id);
            return View(News);
        }

    }
}