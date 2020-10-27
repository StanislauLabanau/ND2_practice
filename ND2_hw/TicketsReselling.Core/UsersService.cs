using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicketsReselling.DAL;
using TicketsReselling.DAL.Models;

namespace TicketsReselling.Core
{
    public class UsersService
    {
        private readonly TicketsResellingContext context;

        public UsersService(TicketsResellingContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = context.Users;

            return await users.ToListAsync();
        }

        public async Task<User> GetUserById(string id)
        {
            return await context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByUserName(string userName)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.UserName.Equals(userName));
        }

        public async Task<string> GetUserFullName(string id)
        {
            var user = await GetUserById(id);
            return user.FirstName + user.SecondName;
        }
    }
}
