using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketsReselling.DAL.Models
{
    public class User: IdentityUser
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Localization { get; set; }
        public string Address { get; set; }

    }
}
