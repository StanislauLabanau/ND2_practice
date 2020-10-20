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
        public static int EventIdCounter { get; set; } = 7;

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
                new Category {Id = 0, Name = "All categories"},
                new Category {Id = 1, Name = "Concerts"},
                new Category {Id = 2, Name = "Sports"},
                new Category {Id = 3, Name = "Exhibitions"}
            };

            events = new List<Event>
            {
                new Event {Id = 1, Category = categories[1], Name = "Concert1", Venue = venues[0], Banner = "ImagineD1.jpg",
                    Description = "<div class=\"text-break\"> Concert1 description </></div>", Date = new DateTime(2022,01,22)},
                new Event {Id = 2, Category = categories[1], Name = "Concert2", Venue = venues[2], Banner = "ImagineD2.jpg",
                    Description = "<div class=\"text-break\"> Concert2 description </></div>", Date = new DateTime(2022,02,22)},
                new Event {Id = 3, Category = categories[1], Name = "Concert3", Venue = venues[3], Banner = "ImagineD3.jpg",
                    Description = "<div class=\"text-break\"> Concert3 description </></div>", Date = new DateTime(2022,03,22)},
                new Event {Id = 4, Category = categories[1], Name = "Concert4", Venue = venues[4], Banner = "ImagineD4.jpg",
                    Description = "<div class=\"text-break\"> Concert4 description </></div>", Date = new DateTime(2022,04,22)},
                new Event {Id = 5, Category = categories[2], Name = "Sports1", Venue = venues[9], Banner = "Foot1.jpg",
                    Description = "<div class=\"text-break\"> Sports1 description </></div>", Date = new DateTime(2022,05,22)},
                new Event {Id = 6, Category = categories[2], Name = "Sports2", Venue = venues[10], Banner = "Foot2.jpg",
                    Description = "<div class=\"text-break\"> Sports2 description </></div>", Date = new DateTime(2022,06,22)},
                new Event {Id = 7, Category = categories[3], Name = "Exhibition1", Venue = venues[11], Banner = "Exhibition1.jpg",
                    Description = "<div class=\"text-break\"> Exhibition1 description </></div>", Date = new DateTime(2022,07,22)},
            };
        }

        public Category[] GetCategories()
        {
            return categories.ToArray();
        }

        public Category GetCategoryById(int id)
        {
            return categories.FirstOrDefault(c => c.Id == id);
        }

        public Event[] GetEvents()
        {
            return events.ToArray();
        }

        public Event GetEventById(int id)
        {
            return events.FirstOrDefault(p => p.Id == id);
        }

        public Venue[] GetVenues()
        {
            return venues.ToArray();
        }

        public Venue GetVenueById(int id)
        {
            return venues.FirstOrDefault(v => v.Id == id);
        }

        public void AddEvent(Event newEvent)
        {
            events.Add(newEvent);
        }

        public void RemoveEvent(int Id)
        {
            events.Remove(GetEventById(Id));
        }
    }
}
