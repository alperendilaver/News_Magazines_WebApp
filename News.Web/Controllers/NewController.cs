using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using News.Data.Data.Models;
using News.Data.DTOs.NewDTOs;
using News.Data.Services.AbstractServices;
using News.Web.Models.New;
namespace News.Web.Controllers
{
  
    public class NewController : Controller
    {   

        private readonly IUserService _userService;
        private readonly ICategoryService _categoryService;
        private readonly INotyfService _notyf;
        private readonly INewService _newService;
        private IImageService _imageService;
        private ISuperAdminService _superAdminService;
        public NewController(ISuperAdminService superAdminService, IImageService imageService,INewService newService,INotyfService notyfService,ICategoryService categoryService,IUserService userService)
        {
            _userService = userService;
            _categoryService = categoryService;
            _notyf = notyfService;
            _newService = newService;
            _imageService= imageService;
            _superAdminService = superAdminService;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        public async Task<IActionResult> CreateNew(){
        
            var auth= await _userService.GetAuthor(Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            var role = User.FindFirstValue(ClaimTypes.Role);

            var Cats= await _categoryService.GetAuthorCategories(auth.UserId, role);
            
            ViewBag.Categories = new SelectList(Cats,"CategoryId","CategoryName");
        
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateNew(CreateNewViewModel model){
            if(ModelState.IsValid){
               var image = await _imageService.UploadImageAsync(model.ImageFile);
               var entity= new New{
                UserId=model.AuthorId,
                CategoryId = model.CategoryId,
                Context= model.context,
                Tittle = model.tittle,
                Image = image,
                PublishedTime = DateTime.UtcNow.AddHours(3),
                IsActive = true
            };
            var result = await _newService.CreateNew(entity);
            if(result.Success){
                _notyf.Success(result.Message);
                return RedirectToAction("CreateNew","New");
            }
            else{
                _notyf.Warning(result.Message);
                return View();
            }
            
        }
        _notyf.Error("Bütün Bilgiler Doldurulmalıdır");
        return RedirectToAction("CreateNew","New");
        }
        
        public async Task<IActionResult> Detail(int Id){
            var New = await _newService.GetNewForDetail(Id);
            var NewItem = await _newService.GetRandomNew();
            var comments = await _newService.GetCommentsForNew(Id);
            var ViewModel = new DetailViewModel{
                New = New,
                RandomNews =NewItem.RandomNews,
                Comments = comments
            };
            return View(ViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> GetCommentsForNew(GetCommentsDTO getCommentsDTO){
            var New = await _newService.GetNewForDetail(getCommentsDTO.NewId);
            var NewItem = await _newService.GetRandomNew();
            var comments = await _newService.GetCommentsForNew(getCommentsDTO.NewId);
            var ViewModel = new DetailViewModel{
                New = New,
                RandomNews =NewItem.RandomNews,
                Comments = comments
            };
            return PartialView("_GetCommentsForNew",ViewModel);    
        }
        public async Task<IActionResult> GetRandomNew(){
            var NewItem = await _newService.GetRandomNew();
            return PartialView("_RandomNewsPartial",NewItem.RandomNews);
        }
        public async Task<IActionResult> Edit(int Id){
            var New =await  _newService.GetNewForEdit(Id);
            if(New == null){
                _notyf.Error("Haber Yüklenemedi");
                return RedirectToAction("Detail","New",new {Id});
            }
            var NewEditViewModel = new EditNewViewModel{
                CategoryId = New.Category.CategoryId,
                Image = New.Image,
                Context = New.Context,
                Tittle = New.Tittle,
                NewId = New.NewId,
        
            };
            return View(NewEditViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditNewViewModel editNewViewModel){
            var newDto = new EditNewDTO{
                NewId = editNewViewModel.NewId,
                CategoryId = editNewViewModel.CategoryId,
                Context = editNewViewModel.Context,
                Image = editNewViewModel.Image,
                ImageFile = editNewViewModel.ImageFile,
                Tittle = editNewViewModel.Tittle,
            };
            var result = await _newService.UpdateNew(newDto);
            if(result.Success){
                _notyf.Success(result.Message);
                return RedirectToAction("Detail","New",new{Id = editNewViewModel.NewId});
            }
            _notyf.Error(result.Message);
            return View(editNewViewModel);
        }     

        public async Task<IActionResult> Delete(int NewId){
            var result = await _newService.DeleteNew(NewId);
            if(result.Success){
                _notyf.Success(result.Message);
                return RedirectToAction("ViewNews","SuperAdmin",new{Id= Int32.Parse( User.FindFirstValue(ClaimTypes.NameIdentifier))});
            }
            else{
                _notyf.Error(result.Message);
                return RedirectToAction("Index","Home");
            }
        }

        public async Task<IActionResult> PublishNew(int Id){
            var result = await _newService.PublishNew(Id);
            if(result.Success){
                _notyf.Success(result.Message);
            }
            else
                _notyf.Error(result.Message);
            return RedirectToAction("Profile","Users",new{Id=User.FindFirstValue(ClaimTypes.NameIdentifier)});
        }
        public async Task<IActionResult> UnpublishNew(int Id){
            var result = await _newService.UnpublishNew(Id);
            if(result.Success){
                _notyf.Warning(result.Message);
            }
            else
                _notyf.Error(result.Message);
            return RedirectToAction("Profile","Users",new{Id=User.FindFirstValue(ClaimTypes.NameIdentifier)});
        }
        
        public async Task< IActionResult> FilteredNew(string filter){
            return View(await _newService.GetFilteredNews(filter));
        }

        [HttpPost]
        public async Task<JsonResult> AddComment(CreateCommentViewModel viewModel){
            var UserName = $"{User.FindFirstValue(ClaimTypes.Name)} {User.FindFirstValue(ClaimTypes.GivenName)}";
            var result = await _newService.CreateComment(new CreateCommentDTO{
                Context=viewModel.Context, 
                PublishedDate=DateTime.UtcNow.AddHours(3),
                NewId=viewModel.NewId,
                UserId = viewModel.UserId,
            });
           
            var PublishedDate = result.Data.PublishedDate.ToString("dd/MM/yyyy hh:mm");
            return Json(new{
                result.Data.Context,PublishedDate,result.Data.User,UserName,result.Data.CommentId,message =result.Message
            });
        }
       
        public async Task<JsonResult> AddReactionToComment(int ComId,string ReactionType,string ComType){
            var reactiontype = true ? ReactionType == "true" : false;
            var result = await _newService.AddReactionToComment(ComId,reactiontype, ComType);

            return Json(new{
                result.Data.LikeCount,
                result.Data.DisslikeCount,
                message = result.Message
            }
            );
        }

        public async Task<JsonResult> AddReplyToComment(AddReplyDTO replyDTO){
            var result = await _newService.AddReplyToComment(replyDTO);
            var UserName = $"{User.FindFirstValue(ClaimTypes.Name)} {User.FindFirstValue(ClaimTypes.GivenName)}";
            
            var publishedDate = result.Data.PublishedDate.ToString("dd/MM/yyyy hh:mm"); 
            return Json(new{result.Data.Text, result.Data.ReplyId,UserName,publishedDate,message = result.Message});
        }

        public async Task<JsonResult> DeleteComment(DeleteCommentDTO deleteCommentDTO){
            var result = await _newService.DeleteComment(deleteCommentDTO.CommentId,deleteCommentDTO.Type,deleteCommentDTO.WarningId);
            return Json(new {CommentId = result.Data, message=result.Message});
        }

        public async Task<JsonResult> AddReactionToNew(AddReactionDTO addReactionDTO){
            var result = await _newService.AddReaction(addReactionDTO);     
        
            return Json(new{likecount=result.Data.LikeCount,disslikecount=result.Data.DisslikeCount,message= result.Message}); 
        }
    }
}