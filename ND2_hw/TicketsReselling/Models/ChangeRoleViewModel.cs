using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketsReselling.Models
{
    public class ChangeRoleViewModel
    {
        public IEnumerable<UserWithRoles> UsersWithRoles { get; set; }
        public IEnumerable<IdentityRole> AllRoles { get; set; }
    }

    public class UserWithRoles
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public IList<string> UserRoles { get; set; }
    }
}
