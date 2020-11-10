using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicketsReselling.DAL;
using TicketsReselling.DAL.Enums;
using TicketsReselling.DAL.Models;

namespace TicketsReselling.Core.Interfaces
{
    public interface ICitiesService
    {
        public Task<IEnumerable<City>> GetCities();
        public Task<City> GetCityById(int id);
        public Task AddCity(City newCity);
        public Task RemoveCity(int Id);
        public Task<IEnumerable<City>> GetCityesByStatus(params CityStatuses[] statuses);
        public Task ChangeCityStatus(City city, CityStatuses status);
        public Task MakeCityAndAllItsVenuesNotAvaliable(int Id);
        public Task MakeCityAndAllItsVenuesAvaliable(int Id);
    }
}