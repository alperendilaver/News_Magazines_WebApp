using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace News.Web.Controllers
{
   public class ErrorController : Controller
{
    [Route("Home/Error")]
    public IActionResult Error()
    {
        return View();
    }
}
}