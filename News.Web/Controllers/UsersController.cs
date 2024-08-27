using News.Web.Models.Users;
using Microsoft.AspNetCore.Mvc;

using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using News.Data.Services.AbstractServices;
using News.Data.DTOs.UserDTOs;
using News.Data.Services.ConcreateServices;

namespace News.Web.Controllers
{   public class UsersController : Controller
    {
        private IClaimService _claimService;
        
        private readonly INotyfService _notyf;
        private readonly IUserService _userService;
         private readonly INewService _newService;
        private readonly ITokenService _tokenService;
        public UsersController(IUserService userService,ITokenService tokenService,INotyfService notyfService,INewService newService,IClaimService claimService)
        {
            _newService = newService;
            _notyf =notyfService;
            _tokenService =tokenService;
            _claimService= claimService;
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register(){
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel){
            if(ModelState.IsValid){
                var registerDTO = new UserRegisterDTO{
                    ConfirmPassword = registerViewModel.ConfirmPassword,
                    FirstName= registerViewModel.FirstName,
                    SurName = registerViewModel.SurName,
                    Mail = registerViewModel.Mail,
                    Password = registerViewModel.Password,
                    PhoneNumber = registerViewModel.PhoneNumber,
                    RoleId = registerViewModel.RoleId,
                };
                var result = await _userService.Register(registerDTO);
                if (result.Success)
                {
                    _notyf.Information("Kullanıcı oluşturma başarılı. Mail hesabınıza gelen link ile doğrulama yapmanız gerekmektedir.");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }

        return View(registerViewModel);
        }

        public IActionResult Login(){
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel){
            if(ModelState.IsValid){
                var loginDTO = new UserLoginDTO{
                    Mail = loginViewModel.Mail,
                    Password = loginViewModel.Password,
                    RememberMe = loginViewModel.RememberMe,
                };
                var result = await _userService.Login(loginDTO);
                if(result !=null && !String.IsNullOrEmpty(result.Email)){
                   await _claimService.GenerateClaim(result.UserId,result.FirstName,result.LastName,result.PhoneNumber,loginViewModel.RememberMe,result.Role.RoleName);
                    return RedirectToAction("Index","Home");
                }
                else{
                    ModelState.AddModelError("","Giriş Başarısız");
                }
            }
            return View(loginViewModel);
        }

        
        public async Task<IActionResult> Logout(){
            await _claimService.DeleteClaim();
            return RedirectToAction("Index","Home");    
        }
        
        public async Task<IActionResult> ConfirmEmail(int UserId,string Token){
            var result = await _tokenService.VerifyToken(UserId, Token);
            if(result){
                _notyf.Success("Email Başarı ile doğrulandı"); 
                return RedirectToAction("Index","Home");
            }
            ModelState.AddModelError("","Mail Doğrulanamadı");
            return View();   
        }

        public async Task<IActionResult> SendResetPasswordMail(string Email){
            var user = await  _userService.GetUser(Email);
            await _userService.SendResetPassword(user);
            _notyf.Information("Mail Adresinize Gelen Bağlantı ile şifrenizi sıfırlayabilirsiniz");
            return RedirectToAction("Index","Home");
        }
        public IActionResult InputEmailForPasswordReset(){
            return View();
        }
        public IActionResult ResetPassword(int UserId,string token){
            ViewBag.Id=UserId; 
            ViewBag.token = token;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(string password,string confirmpassword,int UserId,string token){
            
            //email ile al ona göre ayarla
            if(password == null || confirmpassword == null){
                _notyf.Error("Parolalar Birbiri ile eşleşmiyor");
                return View();
            }
            else{
                var result = await _userService.ResetPassword(UserId,password,token);
                if(result.Success){
                    _notyf.Success(result.Message);
                    return RedirectToAction("Login","Users");
                }
                else{
                    _notyf.Warning(result.Message);
                    return View();
                }
                
            }
        } 
        public async Task<IActionResult> Profile(int Id){
            var user = await _userService.GetUser(Id);
            if(user.RoleId==3)
                return RedirectToAction("ViewNews","SuperAdmin");
            return View(user);
           
        }

        public async Task<IActionResult> Delete(int Id){
            var result = await _userService.Delete(Id);
            if(result.Success)
                _notyf.Success(result.Message);
            else
                _notyf.Warning(result.Message);

            return RedirectToAction("ViewUsers","SuperAdmin");
        }

        public async Task<IActionResult> ViewNotify(int UserId){
            return View(await _userService.GetNotifies(UserId));
        }

        public async Task<IActionResult> GetNotifies(int UserId){
            return PartialView("_GetNotifiesPartial",await _userService.GetNotifies(UserId));
        }
        [HttpPost]
        public async Task<JsonResult> MarkAsReadNotify(int NotifyId){
            var result = await _userService.MarkAsRead(NotifyId);
            return Json(new{notifyid=result.Data});
        }
        [HttpPost]
        public async Task<JsonResult> DeleteNotify(int NotifyId){
            var result = await _userService.DeleteNotify(NotifyId);
            return Json(new {notifyid=result.Data});
        }
    }
}