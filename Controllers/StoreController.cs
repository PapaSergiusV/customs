using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Threading;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using customs.ViewModels;

namespace customs.Controllers
{
    [Authorize]
    public class StoreController : Controller
    {
        private readonly ILogger<StoreController> _logger;
        private IFileService files;
        // private Models.User CurrentUser;

        public StoreController(ILogger<StoreController> logger, IFileService service)
        {
            files = service;
            _logger = logger;
        }

        public IActionResult Index()
        {
            DateTime now = DateTime.UtcNow;
            ViewBag.Files = files.All().Where(f => f.UserId == CurrentUserId()).Select(f => new FileView(f)).ToArray();
            @ViewData["UserEmail"] = User.Identity.Name;
            return View();
        }

        [HttpGet]
        public IActionResult Upload()
        {
            @ViewData["UserEmail"] = User.Identity.Name;
            return View();
        }

        [HttpGet]
        public IActionResult Download(int id)
        {
            Models.File file = files.Find(id);
            if (file.UserId != CurrentUserId())
                return RedirectToAction("Index");
            string path = file.Path;
            return File(System.IO.File.ReadAllBytes(path), "application/octet-stream", Path.GetFileName(path));
        }
        
        [HttpPost]
        public IActionResult Upload(IFormFile file, int hours)
        {
            files.Save(file, hours, CurrentUserId());
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            files.Destroy(id);
            return RedirectToAction("Index");
        }

        [NonAction]
        public int CurrentUserId()
        {
            string userId = User.Claims.Where(c => c.Type == "id").Select(c => c.Value).SingleOrDefault();
            return int.Parse(userId);
        }
    }
}
