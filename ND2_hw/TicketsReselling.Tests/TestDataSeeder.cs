using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicketsReselling.DAL;
using TicketsReselling.DAL.Enums;
using TicketsReselling.DAL.Models;

namespace TicketsReselling.Tests
{
    class TestDataSeeder
    {
        private readonly TicketsResellingContext context;
        private readonly List<City> cities;
        private readonly List<Venue> venues;
        private readonly List<Category> categories;
        private readonly List<Event> events;
        private readonly List<Ticket> tickets;

        public TestDataSeeder(TicketsResellingContext context)
        {
            this.context = context;

            cities = new List<City>
            {
                new City {Name = "Tallin", Status = CityStatuses.NotAvaliable},
                new City {Name = "Vilnius", Status = CityStatuses.Avaliable},
                new City {Name = "Riga", Status = CityStatuses.Avaliable},
            };

            venues = new List<Venue>
            {
                new Venue {Name = "TallinVenueA", City = cities[0], Address = "TallinVenueAAddres", Status = VenueStatuses.NotAvaliable},
                new Venue {Name = "TallinVenueB", City = cities[0], Address = "TallinVenueBAddres", Status = VenueStatuses.NotAvaliable},
                new Venue {Name = "VilniusVenueA", City = cities[1], Address = "VilniusVenueAAddres", Status = VenueStatuses.Avaliable},
                new Venue {Name = "VilniusVenueB", City = cities[1], Address = "VilniusVenueBAddres", Status = VenueStatuses.Avaliable},
                new Venue {Name = "RigaVenueA", City = cities[2], Address = "RigaVenueAAddres", Status = VenueStatuses.Avaliable},
                new Venue {Name = "RigaVenueB", City = cities[2], Address = "RigaVenueBAddres", Status = VenueStatuses.Avaliable},
            };

            categories = new List<Category>
            {
                new Category {Name = "Concerts"},
                new Category {Name = "Sports"},
                new Category {Name = "Exhibitions"}
            };

            events = new List<Event>
            {
                new Event {Category = categories[0], Name = "Concert1", Status = EventStatuses.Current, Venue = venues[0], Banner = "ImagineD1.jpg",
                    Description = "<div class=\"text-break\"> Concert1 description </></div>", Date = new DateTime(2022,01,01)},
                new Event {Category = categories[0], Name = "Concert2", Status = EventStatuses.Current, Venue = venues[1], Banner = "ImagineD2.jpg",
                    Description = "<div class=\"text-break\"> Concert2 description </></div>", Date = new DateTime(2022,02,02)},
                new Event {Category = categories[0], Name = "Concert3", Status = EventStatuses.Current, Venue = venues[2], Banner = "ImagineD3.jpg",
                    Description = "<div class=\"text-break\"> Concert3 description </></div>", Date = new DateTime(2022,03,03)},
            };

            tickets = new List<Ticket>
            {
                new Ticket {EventId = 1, Status = TicketStatuses.Selling, Price = 110, SellerId = "1", SellerNotes="Notes" },
                new Ticket {EventId = 1, Status = TicketStatuses.Selling, Price = 100, SellerId = "1", SellerNotes="Notes" },
                new Ticket {EventId = 1, Status = TicketStatuses.Selling, Price = 110, SellerId = "2", SellerNotes="Notes" },
            };
        }

        public void SeedData()
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Categories.AddRange(categories);
            context.Events.AddRange(events);
            context.Tickets.AddRange(tickets);
            context.Venues.AddRange(venues);
            context.SaveChanges();
        }
    }
}
