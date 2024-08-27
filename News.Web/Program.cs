using System.Security.Claims;
using AspNetCoreHero.ToastNotification;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using News.Data.Data.Context;
using News.Data.Services.AbstractServices;
using News.Data.Services.ConcreateServices;
using News.Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

builder.Services.AddAuthorization(options=>{
    options.AddPolicy("SuperAdmin",policy=>policy.RequireClaim(ClaimTypes.Role,"SuperAdmin"));
    options.AddPolicy("Editor",policy=>policy.RequireClaim(ClaimTypes.Role,"Editor"));
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options=>
{
    options.LoginPath="/Users/Login";  
});//cookie kullanacağımızı uygulamaya söyledik
// Add services to the container.
builder.Services.AddDbContext<NewsContext>(options=>{
    var config = builder.Configuration;
    var connectionString=config.GetConnectionString("mssql");
    options.UseSqlServer(connectionString,b=>b.MigrationsAssembly("News.Web"));
});
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddSingleton<IUrlHelperFactory, UrlHelperFactory>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IClaimService, ClaimService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IRequestService, RequestService>();
builder.Services.AddSingleton<IHashService>(provider => HashService.Instance);
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ISuperAdminService,SuperAdminService>();
builder.Services.AddScoped<IUserNotifyService, UserNotifyService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<INewService,NewService>();
builder.Services.AddScoped<IChannelService, ChannelService>();
builder.Services.AddSingleton<IEmailService,EmailService>(i=>new EmailService(
    builder.Configuration["EmailSender:Host"],
    builder.Configuration.GetValue<int>("EmailSender:Port"),
    builder.Configuration.GetValue<bool>("EmailSender:EnableSSL"),
    builder.Configuration["EmailSender:UserName"],
    builder.Configuration["EmailSender:Password"]

));
builder.Services.AddNotyf(config=> { config.DurationInSeconds = 10;config.IsDismissable = true;config.Position = NotyfPosition.BottomRight; });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseMiddleware<CustomExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication(); // UseAuthentication eklenmelidir
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
