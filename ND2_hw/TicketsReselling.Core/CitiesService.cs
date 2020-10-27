using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicketsReselling.DAL;
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

        public async Task<City> GetCityById(int id)
        {
            return await context.Cities.FindAsync(id);
        }

        public async Task AddCity(City newCity)
        {
            context.Cities.Add(newCity);
            await context.SaveChangesAsync();
        }

        public async void RemoveCity(int Id)
        {
            context.Cities.Remove(await GetCityById(Id));
            await context.SaveChangesAsync();
        }
    }
}
