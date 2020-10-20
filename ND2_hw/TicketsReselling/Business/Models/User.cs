using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketsReselling.Business.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Localization { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public static string GetFullName (User user)
        {
            return $"{user?.FirstName} {user?.SecondName}";
        }
    }
}
