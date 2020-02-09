using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using customs.Models;
using customs.ViewModels;

namespace customs
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private IUserService _userService;

        public AccountController(ILogger<AccountController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SigninView model)
        {
            if (ModelState.IsValid)
            {
                User user = _userService.SignIn(model);
                if (user != null)
                {
                    await Authenticate(user.Email, user.Id.ToString());
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Wrong email and/or password");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignupView model)
        {
            if (ModelState.IsValid)
            {
                if (!_userService.Exists(model))
                {
                    User user = _userService.SignUp(model);
                    await Authenticate(user.Email, user.Id.ToString());
                    return RedirectToAction("index", "Home");
                }
                ModelState.AddModelError("", "Email occupied");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("SignIn");
        }

        [HttpGet]
        public async Task<IActionResult> Destroy()
        {
            _userService.Destroy(User.Identity.Name);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("SignIn");
        }

        [NonAction]
        private async Task Authenticate(string userName, string userId)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
                new Claim("id", userId)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
