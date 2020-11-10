using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicketsReselling.Core.Interfaces;

namespace TicketsReselling.Core
{
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Service administration", "TicketResellingAdm@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync("ticketresellingadm@gmail.com", "ticketreselling");
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
