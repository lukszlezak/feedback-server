using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Gmtl.Feedback.Server.Extensions;
using Gmtl.Feedback.Server.Models;
using Gmtl.Feedback.Server.Services;
using Gmtl.Feedback.Server.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gmtl.Feedback.Server.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserService userService;
        private readonly FeedbackService feedbackService;

        public HomeController(UserService userService, FeedbackService feedbackService)
        {
            this.userService = userService;
            this.feedbackService = feedbackService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginViewModel model = new LoginViewModel();

            return View(model);
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            var user = userService.AuthenticateUser(model.Login, model.Password);

            if (user != null)
            {
                CreateCookieAsync(user);

                return RedirectToAction("DashBoard", "Feedback");
            }

            model.ErrorMessage = "Not correct login or password";

            return View(model);
        }

        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).ConfigureAwait(false);

            return RedirectToAction("Index", "Home");
        }

        public async void CreateCookieAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Login)
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties();

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties).ConfigureAwait(false);

            HttpContext.Session.SetString(IdentityCustomKeys.Name, user.Login);
            HttpContext.Session.Set<Guid>(IdentityCustomKeys.Id, user.Id);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                userService.AddUser(new User { Id = Guid.NewGuid(), Login = model.Login, Password = model.Password });

                return RedirectToAction("Index");
            }

            return View();
        }
    }
}