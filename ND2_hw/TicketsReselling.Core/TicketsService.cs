using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsReselling.DAL;
using TicketsReselling.DAL.Models;
using TicketsReselling.DAL.Enums;
using TicketsReselling.Core.Interfaces;

namespace TicketsReselling.Core
{
    public class TicketsService : ITicketsService
    {
        private readonly TicketsResellingContext context;

        public TicketsService(TicketsResellingContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Ticket>> GetTickets()
        {
            var tickets = context.Tickets;

            return await tickets.ToListAsync();
        }

        public async Task<Ticket> GetTicketById(int id)
        {
            return await context.Tickets.FindAsync(id);
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByUserId(string userId)
        {
            var userTickets = context.Tickets.Where(t => t.SellerId.Equals(userId));

            return await userTickets.ToListAsync();
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByEventIdAndStatus(int eventId, TicketStatuses status)
        {
            var eventTickets = context.Tickets.Where(t => t.EventId == eventId && t.Status == status);

            return await eventTickets.ToListAsync();
        }

        public async Task ChangeTicketStatus(Ticket ticket, TicketStatuses status)
        {
            ticket.Status = status;
            context.Tickets.Update(ticket);
            await context.SaveChangesAsync();
        }

        public async Task AddTicket(Ticket newTicket)
        {
            context.Tickets.Add(newTicket);
            await context.SaveChangesAsync();
        }

        public async Task RemoveTicket(int id)
        {
            context.Tickets.Remove(await GetTicketById(id));
            await context.SaveChangesAsync();
        }
    }
}
