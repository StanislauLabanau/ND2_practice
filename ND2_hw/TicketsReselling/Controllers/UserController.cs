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

namespace TicketsReselling.Controllers
{
    public class UserController : Controller
    {
        private readonly UsersRepository usersRepository;

        public UserController(UsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
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

            if (user==null)
            {
                ModelState.AddModelError(nameof(loginModel.UserName), "User not found");
                return View();
            }

            if (!user.Password.Equals(loginModel.Password))
            {
                ModelState.AddModelError(nameof(loginModel.UserName), "Wrong password");
                return View();
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
                return Redirect(returnUrl);
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