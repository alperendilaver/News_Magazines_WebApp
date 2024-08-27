using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipelines;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using News.Data.DTOs.NewDTOs;
using News.Data.Services.AbstractServices;

namespace News.Web.Controllers
{
  
    public class CommentController : Controller
    {
        private INotyfService _notyfService;
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService,INotyfService notyfService)
        {
            _notyfService = notyfService;
            _commentService = commentService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SendWarningForComment(SendWarningForCommentDTO sendWarningForCommentDTO){
            var result = await _commentService.SendWarningForComment(sendWarningForCommentDTO);
        

            return Json(new {result.Message});
            }


        public async Task<JsonResult> RemoveWarning(WarningItems warningItems){
                var result = await _commentService.RemoveWarning(warningItems.WarningId);
                
                return Json(new{result.Data,message=result.Message});
        }
        }
        public class WarningItems{
            public int WarningId { get; set; }
        }
    }
