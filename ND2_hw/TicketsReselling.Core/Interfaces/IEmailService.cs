using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TicketsReselling.Core.Interfaces
{
    public interface IEmailService
    {
        public Task SendEmailAsync(string email, string subject, string message);
    }
}
