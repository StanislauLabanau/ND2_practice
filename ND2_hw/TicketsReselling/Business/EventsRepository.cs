using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsReselling.Business.Models;

namespace TicketsReselling.Business
{
    public class EventsRepository
    {
        private readonly List<City> cities;
        private readonly List<Venue> venues;
        private readonly List<Category> categories;
        private readonly List<Event> events;

        public EventsRepository()
        {
            cities = new List<City>
            {
                new City {Id = 1, Name = "Tallin"},
                new City {Id = 2, Name = "Vilnius"},
                new City {Id = 3, Name = "Riga"},
                new City {Id = 4, Name = "Warsaw"},
                new City {Id = 5, Name = "Praga"},
                new City {Id = 6, Name = "Bratislava"},
            };

            venues = new List<Venue>
            {
                new Venue {Id = 1, Name = "TallinVenueA", City = cities[0], Address = "TallinVenueAAddres"},
                new Venue {Id = 2, Name = "TallinVenueB", City = cities[0], Address = "TallinVenueBAddres"},
                new Venue {Id = 3, Name = "VilniusVenueA", City = cities[1], Address = "VilniusVenueAAddres"},
                new Venue {Id = 4, Name = "VilniusVenueB", City = cities[1], Address = "VilniusVenueBAddres"},
                new Venue {Id = 5, Name = "RigaVenueA", City = cities[2], Address = "RigaVenueAAddres"},
                new Venue {Id = 6, Name = "RigaVenueB", City = cities[2], Address = "RigaVenueBAddres"},
                new Venue {Id = 7, Name = "WasawVenueA", City = cities[3], Address = "WasawVenueAAddres"},
                new Venue {Id = 8, Name = "WasawVenueB", City = cities[3], Address = "WasawVenueBAddres"},
                new Venue {Id = 9, Name = "PragaVenueA", City = cities[4], Address = "PragaVenueAAddres"},
                new Venue {Id = 10, Name = "PragaVenueB", City = cities[4], Address = "PragaVenueBAddres"},
                new Venue {Id = 11, Name = "BratislavaVenueA", City = cities[5], Address = "BratislavaVenueAAddres"},
                new Venue {Id = 12, Name = "BratislavaVenueB", City = cities[5], Address = "BratislavaVenueBAddres"}
            };

            categories = new List<Category>
            {
                new Category {Id = 0, Name = "Concerts"},
                new Category {Id = 1, Name = "Sports"},
                new Category {Id = 2, Name = "Exhibitions"}
            };

            events = new List<Event>
            {
                new Event {Id = 1, Category = categories[0], Name = "Concert1", Venue = venues[0], Banner = "ImagineD1.jpg", Description ="Concert1 description", Date = "22.01.2022"},
                new Event {Id = 2, Category = categories[0], Name = "Concert2", Venue = venues[2], Banner = "ImagineD2.jpg", Description ="Concert2 description", Date = "22.02.2022"},
                new Event {Id = 3, Category = categories[0], Name = "Concert3", Venue = venues[3], Banner = "ImagineD3.jpg", Description ="Concert3 description", Date = "22.03.2022"},
                new Event {Id = 4, Category = categories[0], Name = "Concert4", Venue = venues[4], Banner = "ImagineD4.jpg", Description ="Concert4 description", Date = "22.04.2022"},
                new Event {Id = 5, Category = categories[1], Name = "Sports1", Venue = venues[9], Banner = "Foot1.jpg", Description ="Sports1 description", Date = "22.05.2022"},
                new Event {Id = 6, Category = categories[1], Name = "Sports2", Venue = venues[10], Banner = "Foot2.jpg", Description ="Sports2 description", Date = "22.06.2022"},
                new Event {Id = 7, Category = categories[2], Name = "Exhibition1", Venue = venues[11], Banner = "Exhibition1.jpg", Description ="Exhibition1 description", Date = "22.07.2022"},
            };
        }

        public Category[] GetCategories()
        {
            return categories.ToArray();
        }

        public Event[] GetEvents()
        {
            return events.ToArray();
        }

        public Event GetEventById(int id)
        {
            return events.FirstOrDefault(p => p.Id == id);
        }
    }
}
