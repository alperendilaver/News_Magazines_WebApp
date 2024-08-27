using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using News.Web.Models.Editors;
using News.Web.Models.Requests;
using News.Web;
using News.Data.Services.AbstractServices;

namespace News.Web.Controllers
{
    [Authorize(Policy = "SuperAdmin")]
    public class SuperAdmin : Controller
    {

        private readonly ICategoryService _categoryService;
        private readonly IRequestService _requestService;
        private readonly INewService _newService;
        private readonly INotyfService _notyf;
        private ISuperAdminService _superAdminService;
        private IUserService _userService;

        private ICommentService _commentService;
        public SuperAdmin(IUserService userService,ISuperAdminService superAdminService,INotyfService notyf,INewService newService,IRequestService requestService,ICategoryService categoryService,ICommentService commentService)
        {
            _commentService=commentService;
            _categoryService = categoryService;
            _requestService = requestService;
            _newService= newService;
            _notyf = notyf;
            _superAdminService = superAdminService;
            _userService = userService;
        }
        public IActionResult Index()
            {
            return View();
        }
       

        public async Task<IActionResult> AddEditorOrAuthor(int Id,int role){
            var result = await _superAdminService.AddEditorOrAuthor(Id,role);
            var unvan =  role==2 ?"Author": "Editor";
            if (result.Success)
            {
                _notyf.Success(result.Message);
                return RedirectToAction("ViewUsers");
            }
            else
            {
                _notyf.Warning(result.Message);
                return View();    
            }
        }

        public async Task<IActionResult> AddEditorFromAuthorOrReverse(int UserId,int RoleId){
            var result = await _superAdminService.AddEditorFromAuthorOrReverse(UserId,RoleId);
            if (result.Success)
                _notyf.Success(result.Message);
            else
                _notyf.Warning(result.Message);
            return RedirectToAction("ViewUsers","SuperAdmin");
        }
        public async Task<IActionResult> AddReaderFromEditor(int Id){
            var result = await _superAdminService.AddReaderFromEditor(Id);
             if (result.Success)
            {
                _notyf.Success(result.Message);
                return RedirectToAction("ViewUsers");
            }
            else
            {
                _notyf.Warning(result.Message);
                return View();    
            }
        }

        public async Task< IActionResult> ViewCategories(){
            return View(await _categoryService.GetAllCategories());
        } 

        
        [HttpPost]
        public async Task<IActionResult> AddCategory(string CategoryName){
            var result = await _superAdminService.AddCategory(CategoryName);
            if (result.Success){
                _notyf.Success(result.Message);
                return RedirectToAction("ViewCategories","SuperAdmin");
            }
            _notyf.Warning(result.Message);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int CatId){
            var result = await _superAdminService.DeleteCategory(CatId);
            if (result.Success){
                _notyf.Success(result.Message);
                return RedirectToAction("ViewCategories","SuperAdmin");
            }
            else{
                _notyf.Warning(result.Message);
                return RedirectToAction("ViewCategories","SuperAdmin");
            
            }
        }

        public async Task<IActionResult> ViewNews(){
            return View(await _newService.GetAllNews());
        } 

        [HttpGet]
        public async Task<IActionResult> ViewRequests(){
            var ViewModel = new RequestListViewModel{
                WaitForPublish = await _requestService.GetNewRequests("WaitForPublish"),
                WaitForUpdate = await _requestService.GetNewRequests("WaitForUpdate"),
                CategoryRequests = await _requestService.GetCatRequests()
            };
            return View(ViewModel);
        }

        public async Task<IActionResult> GetAllNewRequests(){
            var ViewModel = new RequestListViewModel{
                WaitForPublish = await _requestService.GetNewRequests("WaitForPublish"),
                WaitForUpdate = await _requestService.GetNewRequests("WaitForUpdate"),
                CategoryRequests = await _requestService.GetCatRequests()
            };
            return PartialView("_AllNewRequestsPartial",ViewModel);
        }

        public async Task<IActionResult> ViewUsers(){
            var viewModel = new EditorsListViewModel{   
                Editors = await _userService.GetUsersByRole("Editor"),
                Readers = await _userService.GetUsersByRole("Reader"),
                Authors = await _userService.GetUsersByRole("Author")
            };
            return View(viewModel);        
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUserRequests(){
            var viewModel = new EditorsListViewModel{   
                Editors=await _userService.GetUsersByRole("Editor"),
                Readers=await _userService.GetUsersByRole("Reader"),
                Authors=await _userService.GetUsersByRole("Author")
            };
            return PartialView("_GetAllUserRequests",viewModel);
        }
        public async Task<IActionResult> ConfirmRequest(int Id,string type){
            var result = await _requestService.ConfirmRequestForPublishNew(Id,type);
            if (result.Success)
                _notyf.Success(result.Message);
            else
                _notyf.Error(result.Message);
            return RedirectToAction("ViewRequests","SuperAdmin");
        }

        public async Task<IActionResult> ConfirmChangeCatRequest(int Id,int role){
            var result = await _requestService.ConfirmRequestForChangeCategory(Id,role);
            if (result.Success)
                _notyf.Success(result.Message);
            else
                _notyf.Error(result.Message);
            return RedirectToAction("ViewRequests","SuperAdmin");
        }
        [HttpPost]
        public async Task<JsonResult> DeleteCatRequest(int ReqId){
            var result = await _requestService.DeleteRequest(ReqId);
            return Json(new{message = result.Message});
        }
        public async Task<IActionResult> ViewComments(){
            return View(await _newService.GetComments());
        }

        public async Task<IActionResult> ViewWarnings(){
            return View(await _commentService.GetWarnings());
        }
        

    }
}