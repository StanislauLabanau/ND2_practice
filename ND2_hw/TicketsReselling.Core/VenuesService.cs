using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicketsReselling.DAL;
using TicketsReselling.DAL.Models;

namespace TicketsReselling.Core
{
    public class VenuesService
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

        public async Task AddVenue(Venue newVenue)
        {
            context.Venues.Add(newVenue);
            await context.SaveChangesAsync();
        }

        public async void RemoveVenue(int Id)
        {
            context.Venues.Remove(await GetVenueById(Id));
            await context.SaveChangesAsync();
        }
    }
}
