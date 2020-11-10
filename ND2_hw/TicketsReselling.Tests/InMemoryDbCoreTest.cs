using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicketsReselling.Core;
using TicketsReselling.DAL;
using TicketsReselling.DAL.Enums;
using TicketsReselling.DAL.Models;
using Xunit;

namespace TicketsReselling.Tests
{
    public class InMemoryDbCoreTest : CoreTest
    {
        public InMemoryDbCoreTest()
        : base(
            new DbContextOptionsBuilder<TicketsResellingContext>()
                .UseInMemoryDatabase("TicketsReselling")
                .Options)
        {
        }

        [Fact]
        public void CanGetCities_CitiesService()
        {
            using (var context = new TicketsResellingContext(ContextOptions))
            {
                var citiesService = new CitiesService(context);

                var cities = citiesService.GetCities().Result.ToList();

                cities.Count().Should().Be(3);
                cities[0].Name.Should().Be("Tallin");
            }
        }

        [Fact]
        public void CanRemoveCity_CitiesService()
        {
            using (var context = new TicketsResellingContext(ContextOptions))
            {
                var citiesService = new CitiesService(context);

                citiesService.RemoveCity(2);

                var cities = citiesService.GetCities().Result.ToList();

                cities.Count().Should().Be(2);
                cities[0].Name.Should().Be("Tallin");
                cities[1].Name.Should().Be("Riga");
            }
        }
        
        [Fact]
        public void CanAddCity_CitiesService()
        {
            using (var context = new TicketsResellingContext(ContextOptions))
            {
                var citiesService = new CitiesService(context);

                var city = new City { Name = "Berlin", Status = CityStatuses.Avaliable };
                citiesService.AddCity(city);
                var cities = citiesService.GetCities().Result.ToList();

                cities.Count().Should().Be(4);
                cities[3].Name.Should().Be("Berlin");
            }
        }

        [Fact]
        public void CanGetCitiesByStatus_CitiesService()
        {
            using (var context = new TicketsResellingContext(ContextOptions))
            {
                var citiesService = new CitiesService(context);

                var citiesAvaliable = citiesService.GetCityesByStatus(CityStatuses.Avaliable).Result.ToList();
                var citiesNotAvaliable = citiesService.GetCityesByStatus(CityStatuses.NotAvaliable).Result.ToList();
                var citiesRemoved = citiesService.GetCityesByStatus(CityStatuses.Removed).Result.ToList();

                citiesAvaliable.Count().Should().Be(2);
                citiesNotAvaliable.Count().Should().Be(1);
                citiesRemoved.Count().Should().Be(0);
                citiesNotAvaliable[0].Name.Should().Be("Tallin");
            }
        }

        [Fact]
        public void CanMakeCityAndAllItsVenuesNotAvaliable_CitiesService()
        {
            using (var context = new TicketsResellingContext(ContextOptions))
            {
                var citiesService = new CitiesService(context);
                var venueService = new VenuesService(context);

                citiesService.MakeCityAndAllItsVenuesNotAvaliable(2);

                var city = citiesService.GetCityById(2).Result;
                var venues = venueService.GetVenuesByStatuses(VenueStatuses.NotAvaliable).Result.ToList();

                city.Status.Should().Be(CityStatuses.NotAvaliable);
                venues.Count.Should().Be(4);
            }
        }

        [Fact]
        public void CanMakeCityAndAllItsVenuesAvaliable_CitiesService()
        {
            using (var context = new TicketsResellingContext(ContextOptions))
            {
                var citiesService = new CitiesService(context);
                var venueService = new VenuesService(context);

                citiesService.MakeCityAndAllItsVenuesAvaliable(1);

                var city = citiesService.GetCityById(1).Result;
                var venues = venueService.GetVenuesByStatuses(VenueStatuses.Avaliable).Result.ToList();

                city.Status.Should().Be(CityStatuses.Avaliable);
                venues.Count.Should().Be(6);
            }
        }
    }
}
