using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace News.Web.Controllers
{   
    
    [Authorize(Policy = "Editor")]
    public class EditorController : Controller
    {
        public IActionResult Profile()
        {
            return View();
        }



    }
}