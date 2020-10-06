using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsReselling.Business.Models;

namespace TicketsReselling.Business
{
    public class UsersRepository
    {
        private readonly List<User> users;

        public UsersRepository()
        {
            users = new List<User>
            {
                new User {Id = 1,  FirstName = "Harry", SecondName="Potter", Localization = "England", Address = "Address1",
                    PhoneNumber="111-11-11", UserName = "Harry", Password = "harry", Role = UserRoles.User },

                new User {Id = 2,  FirstName = "Ron", SecondName="Weasley", Localization = "England", Address = "Address2",
                    PhoneNumber="222-22-22", UserName = "Ron", Password = "ron", Role = UserRoles.User },

                new User {Id = 3,  FirstName = "Hermione", SecondName ="Granger", Localization = "England", Address = "Address3",
                    PhoneNumber="333-33-33", UserName = "Hermione", Password = "hermione", Role = UserRoles.User },

                new User {Id = 4,  FirstName = "Tom", SecondName ="Riddle", Localization = "England", Address = "Address4",
                    PhoneNumber="444-44-44", UserName = "Tom", Password = "tom", Role = UserRoles.User },

                new User {Id = 5, FirstName = "Albus", SecondName = "Dumbledore", Localization = "England", Address = "Hogwarts",
                    PhoneNumber = "555-55-55", UserName = "Albus", Password = "albus", Role = UserRoles.Administrator}
            };
        }

        public User[] GetUsers()
        {
            return users.ToArray();
        }

        public User GetUserById(int id)
        {
            var user = users.FirstOrDefault(p => p.Id == id);
            return user;
        }

        public User GetUserByUserName(string userName)
        {
            var user = users.FirstOrDefault(p => p.UserName == userName);
            return user;
        }
    }
}
