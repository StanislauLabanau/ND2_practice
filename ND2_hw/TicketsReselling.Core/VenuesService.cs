using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsReselling.Core.Interfaces;
using TicketsReselling.DAL;
using TicketsReselling.DAL.Enums;
using TicketsReselling.DAL.Models;

namespace TicketsReselling.Core
{
    public class VenuesService : IVenuesService
    {
        private readonly TicketsResellingContext context;

        public VenuesService(TicketsResellingContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Venue>> GetVenues()
        {
            var venues = context.Venues;

            return await venues.ToListAsync();
        }

        public async Task<Venue> GetVenueById(int id)
        {
            return await context.Venues.FindAsync(id);
        }

        public async Task<IEnumerable<Venue>> GetVenuesByStatuses(params VenueStatuses[] statuses)
        {
            var venues = context.Venues.Where(v => statuses.Contains(v.Status));

            return await venues.ToListAsync();
        }

        public async Task ChangeVenueStatus(Venue venue, VenueStatuses status)
        {
            venue.Status = status;
            context.Venues.Update(venue);
            await context.SaveChangesAsync();
        }

        public async Task AddVenue(Venue newVenue)
        {
            context.Venues.Add(newVenue);
            await context.SaveChangesAsync();
        }

        public async Task RemoveVenue(int Id)
        {
            context.Venues.Remove(await GetVenueById(Id));
            await context.SaveChangesAsync();
        }
    }
}
