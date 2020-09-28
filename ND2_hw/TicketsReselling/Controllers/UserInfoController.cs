using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketsReselling.Business;

namespace TicketsReselling.Controllers
{
    public class UserInfoController : Controller
    {
        private readonly UsersRepository usersRepository;

        public UserInfoController(UsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        [Authorize(Roles = "User")]
        public IActionResult Index()
        {
            var model = usersRepository.GetUserByUserName(User.Identity.Name);

            return View(model);
        }

        public IActionResult PublicInfo(int userId)
        {
            var model = usersRepository.GetUserById(userId);

            return View(model);
        }

    }
}