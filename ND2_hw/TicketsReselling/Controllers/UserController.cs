using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicketsReselling.Business;
using TicketsReselling.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Localization;

namespace TicketsReselling.Controllers
{
    public class UserController : Controller
    {
        private readonly UsersRepository usersRepository;
        private readonly IStringLocalizer<UserController> stringLocalizer;


        public UserController(UsersRepository usersRepository, IStringLocalizer<UserController> stringLocalizer)
        {
            this.usersRepository = usersRepository;
            this.stringLocalizer = stringLocalizer;
        }

        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginModel, string returnUrl)
        {
            var user = usersRepository.GetUserByUserName(loginModel.UserName);

            if (String.IsNullOrEmpty(loginModel.UserName))
            {
                ModelState.AddModelError(nameof(loginModel.UserName), stringLocalizer["FieldIsRequired"]);
                return View(loginModel);
            }

            if (user == null)
            {
                ModelState.AddModelError(nameof(loginModel.UserName), stringLocalizer["UserNotFound"]);
                return View(loginModel);
            }

            if (String.IsNullOrEmpty(loginModel.Password))
            {
                ModelState.AddModelError(nameof(loginModel.Password), stringLocalizer["FieldIsRequired"]);
                return View(loginModel);
            }

            if (!user.Password.Equals(loginModel.Password))
            {
                ModelState.AddModelError(nameof(loginModel.Password), stringLocalizer["WrongPassword"]);
                return View(loginModel);
            }

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, user.Role),
                };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            if (!String.IsNullOrEmpty(returnUrl))
            {
                return LocalRedirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}