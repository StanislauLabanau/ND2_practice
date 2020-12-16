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

        public IEnumerable<Ticket> GetTicketsByEventIdAndStatus(int eventId, TicketStatuses status)
        {
            var eventTickets = context.Tickets.Where(t => t.EventId == eventId && t.Status == status && t.ListingId == null).ToList();
            var listingEventTickets = context.Tickets.Where(t => t.EventId == eventId && t.Status == status && t.ListingId != null);
            var eventListings = context.Listings.Where(l => l.EventId == eventId).ToList();

            foreach (Listing listing in eventListings)
            {
                var firstTicketInListing = listingEventTickets.FirstOrDefault(t => t.ListingId == listing.Id && t.Status == status);
                if (firstTicketInListing!=null)
                {
                    eventTickets.Add(firstTicketInListing);
                }
            }

            return eventTickets;
        }

        public async Task<IEnumerable<Ticket>> GetUserTicketsByEventId(int eventId, string userId)
        {
            var eventTickets = context.Tickets.Where(t => t.EventId == eventId && t.SellerId.Equals(userId) && t.Status != TicketStatuses.Removed)
                .Include(t => t.Listing).ThenInclude(l => l.Event);

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

        public async Task AddListingWithTickets(Ticket newTicket, int amount, string listingName)
        {
            var listing = new Listing { Name = listingName, EventId = newTicket.EventId };
            context.Listings.Add(listing);
            await context.SaveChangesAsync();

            for (int i = 0; i < amount; i++)
            {
                var ticket = new Ticket()
                {
                    EventId = newTicket.EventId,
                    Price = newTicket.Price,
                    SellerId = newTicket.SellerId,
                    Status = newTicket.Status,
                    SellerNotes = newTicket.SellerNotes,
                    Listing = listing
                };

                context.Tickets.Add(ticket);
                await context.SaveChangesAsync();
            }
        }

        public async Task RemoveTicket(int id)
        {
            context.Tickets.Remove(await GetTicketById(id));
            await context.SaveChangesAsync();
        }
    }
}
