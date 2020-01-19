using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using customs.Models;

namespace customs.Controllers
{
    public class StoreController : Controller
    {
        private readonly ILogger<StoreController> _logger;
        private IFileService files;

        public StoreController(ILogger<StoreController> logger, IFileService service)
        {
            files = service;
            _logger = logger;
        }

        public IActionResult Index()
        {
            DateTime now = DateTime.UtcNow;
            ViewBag.Files = files.All().Select(f => new FileView(f.Path, f.Killtime, f.Uploadtime)).ToArray();
            return View();
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Upload(IFormFile file, int hours)
        {
            files.Save(file, hours);
            return RedirectToAction("Index");
        }
    }
}
