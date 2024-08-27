using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using News.Data.Services.AbstractServices;
using News.Web.Models;
using News.Web.Models.Home;


namespace News.Web.Controllers;

public class HomeController : Controller
{

    private readonly IUserService _userService;
    private readonly INewService _newService;
    public HomeController(INewService newService, IUserService userService)
    {
        _userService = userService;
        _newService = newService;
    }
    public async Task<IActionResult> Index()
    {
        var allNews=await _newService.GetConfirmedNews();
        var randomnews =await _newService.GetRandomNews();
        var authors = await _userService.GetAuthors();
        var ViewModel = new ShowNewsViewModel{
            AllNews = allNews,
            Authors = authors,
            FirstRandomCategoryNews = randomnews.first,
            SecondRandomCategoryNews= randomnews.second
        };
        return View(ViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> GetLatestNews()
    {
        // En son haberleri al
        var latestNews = await _newService.GetAllNews();
        var topNews = new ShowNewsViewModel{
            AllNews=latestNews.OrderByDescending(n => n.PublishedTime).Take(5).ToList()
        };

        // Partial View döndür
        return PartialView("_LatestNewsPartial", topNews);
    }
}
