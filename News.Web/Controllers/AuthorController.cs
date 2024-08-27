using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using News.Data.Data.Models;
using News.Data.Services.AbstractServices;
using News.Web;
using News.Web.Models.Requests;
using News.Web;

namespace News.Web.Controllers
{
    public class AuthorController : Controller
    {
        private readonly INotyfService _notyf;

        private readonly IRequestService _requestService;
        private readonly IUserService _userService;
        private readonly ICategoryService _categoryService;
        public AuthorController(IUserService userService,ICategoryService categoryService,IRequestService requestService,INotyfService notyf)
        {
            _notyf = notyf;
            _requestService = requestService;
            _categoryService = categoryService;
            _userService = userService;
        }

        public async Task<IActionResult> Index(){
            var authors= await _userService.GetAuthors();
            return View(authors);
        }
        public async Task<IActionResult> Category(int Id)
        {
            var user = await _userService.GetUserWithAuthor(Id);
            var Categories = await _categoryService.GetAllCategories();
            var catsauth = await _categoryService.GetAuthorCategories(Id,user.Role.RoleName);   
            ViewBag.AuthorCategories= new SelectList(catsauth, "CategoryId","CategoryName");
            ViewBag.Categories = new SelectList(Categories, "CategoryId","CategoryName");
            
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> SendChangeCategoryRequest([FromBody] ChangeCategoryRequestDTO request){
            var Request = new UserCategoryRequest{
                CategoryId= request.CatId,
                UserId = request.UserId
            };
            var result = await _requestService.CreateRequestForCategory(Request);
            if(result.Success){
                _notyf.Success(result.Message);
                return RedirectToAction("Index","Home");
            }
            else{
                _notyf.Warning(result.Message);
                return RedirectToAction("Category","Author",new{Id=request.UserId});
            }
        }
    }

    
}