using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TicketsReselling.Business.Models;
using TicketsReselling.DAL.Models;
using TicketsReselling.Models;

namespace TicketsReselling.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UsersController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<IActionResult> PublicInfo(string userId)
        {
            var model = await userManager.FindByIdAsync(userId);

            return View(model);
        }

        public async Task<IActionResult> UsersWithRoles()
        {
            var users = userManager.Users.ToList();
            var usersWithRoles = new List<UserWithRoles>();

            foreach (User user in users)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                usersWithRoles.Add(new UserWithRoles { UserId = user.Id, UserName = user.UserName, UserRoles = userRoles });
            }

            var model = new ChangeRoleViewModel { AllRoles = roleManager.Roles.ToList(), UsersWithRoles = usersWithRoles };

            return View(model);
        }

        public async Task<IActionResult> GrantAdminRights(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            //var userRoles = await userManager.GetRolesAsync(user);

            //userRoles.Add(UserRoles.Administrator);
            await userManager.AddToRoleAsync(user, UserRoles.Administrator);

            return RedirectToAction("UsersWithRoles", "Users");
        }

        public async Task<IActionResult> RevokeAdminRights(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            //var userRoles = await userManager.GetRolesAsync(user);

            //userRoles.Remove(UserRoles.Administrator);
            await userManager.RemoveFromRoleAsync(user, UserRoles.Administrator);

            return RedirectToAction("UsersWithRoles", "Users");
        }

        public async Task<IActionResult> GrantUserRights(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            //var userRoles = await userManager.GetRolesAsync(user);

            //userRoles.Add(UserRoles.Administrator);
            await userManager.AddToRoleAsync(user, UserRoles.User);

            return RedirectToAction("UsersWithRoles", "Users");
        }

        public async Task<IActionResult> RevokeUserRights(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            //var userRoles = await userManager.GetRolesAsync(user);

            //userRoles.Remove(UserRoles.Administrator);
            await userManager.RemoveFromRoleAsync(user, UserRoles.User);

            return RedirectToAction("UsersWithRoles", "Users");
        }
    }
}