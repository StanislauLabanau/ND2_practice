using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsReselling.DAL;
using TicketsReselling.DAL.Enums;
using TicketsReselling.DAL.Models;

namespace TicketsReselling.Core
{
    public class CitiesService
    {
        private readonly TicketsResellingContext context;

        public CitiesService(TicketsResellingContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<City>> GetCityes()
        {
            var cityes = context.Cities;

            return await cityes.ToListAsync();
        }

        public async Task<IEnumerable<City>> GetCityesByStatus(CityStatuses status)
        {
            var cityes = context.Cities.Where(c=>c.Status == status);

            return await cityes.ToListAsync();
        }

        public async Task<IEnumerable<City>> GetCityesByStatus(CityStatuses status, CityStatuses status1)
        {
            var cityes = context.Cities.Where(c => c.Status == status || c.Status ==status1);

            return await cityes.ToListAsync();
        }

        public async Task<City> GetCityById(int id)
        {
            return await context.Cities.FindAsync(id);
        }

        public async Task ChangeCityStatus(City city, CityStatuses status)
        {
            city.Status = status;
            context.Cities.Update(city);
            await context.SaveChangesAsync();
        }

        public async Task AddCity(City newCity)
        {
            context.Cities.Add(newCity);
            await context.SaveChangesAsync();
        }

        public async Task RemoveCity(int Id)
        {
            context.Cities.Remove(await GetCityById(Id));
            await context.SaveChangesAsync();
        }

        public async Task MakeCityAndAllItsVenuesNotAvaliable(int Id)
        {
            var city = await GetCityById(Id);
            await ChangeCityStatus(city, CityStatuses.NotAvaliable);

            var venues = context.Venues.Where(v => v.CityId == Id);
            foreach (Venue venue in venues)
            {
                venue.Status = VenueStatuses.NotAvaliable;
                context.Venues.Update(venue);
            }

            await context.SaveChangesAsync();
        }

        public async Task MakeCityAndAllItsVenuesAvaliable(int Id)
        {
            var city = await GetCityById(Id);
            await ChangeCityStatus(city, CityStatuses.Avaliable);

            var venues = context.Venues.Where(v => v.CityId == Id);
            foreach (Venue venue in venues)
            {
                venue.Status = VenueStatuses.Avaliable;
                context.Venues.Update(venue);
            }

            await context.SaveChangesAsync();
        }
    }
}
