using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TicketsReselling.Business.Models;
using TicketsReselling.DAL.Models;

namespace TicketsReselling.Controllers
{
    public class UserInfoController : Controller
    {
        private readonly UserManager<User> userManager;

        public UserInfoController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        //[Authorize(Roles = UserRoles.User)]
        //public async Task<IActionResult> Index()
        //{
        //    var model = await userManager.GetUserAsync(User);

        //    return View(model);
        //}

        public async Task<IActionResult> PublicInfo(string userId)
        {
            var model = await userManager.FindByIdAsync(userId);

            return View(model);
        }

    }
}