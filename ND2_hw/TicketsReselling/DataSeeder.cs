using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsReselling.Business;
using TicketsReselling.Business.Models;
using TicketsReselling.DAL;
using TicketsReselling.DAL.Models;
using TicketsReselling.DAL.Enums;

namespace TicketsReselling
{
    public class DataSeeder
    {

        private readonly TicketsResellingContext context;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly List<City> cities;
        private readonly List<Venue> venues;
        private readonly List<Category> categories;
        private readonly List<Event> events;
        private readonly List<Ticket> tickets;
        private readonly List<User> users;
        private readonly List<User> administrators;
        private readonly List<Listing> listings;
        private readonly string[] userPasswords;
        private readonly string[] adminPasswords;

        public DataSeeder(TicketsResellingContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.context = context;

            cities = new List<City>
            {
                new City {Name = "Tallin", Status = CityStatuses.Avaliable},
                new City {Name = "Vilnius", Status = CityStatuses.Avaliable},
                new City {Name = "Riga", Status = CityStatuses.Avaliable},
                new City {Name = "Warsaw", Status = CityStatuses.Avaliable},
                new City {Name = "Praga", Status = CityStatuses.Avaliable},
                new City {Name = "Bratislava", Status = CityStatuses.Avaliable},
            };

            venues = new List<Venue>
            {
                new Venue {Name = "TallinVenueA", City = cities[0], Address = "TallinVenueAAddres", Status = VenueStatuses.Avaliable},
                new Venue {Name = "TallinVenueB", City = cities[0], Address = "TallinVenueBAddres", Status = VenueStatuses.Avaliable},
                new Venue {Name = "VilniusVenueA", City = cities[1], Address = "VilniusVenueAAddres", Status = VenueStatuses.Avaliable},
                new Venue {Name = "VilniusVenueB", City = cities[1], Address = "VilniusVenueBAddres", Status = VenueStatuses.Avaliable},
                new Venue {Name = "RigaVenueA", City = cities[2], Address = "RigaVenueAAddres", Status = VenueStatuses.Avaliable},
                new Venue {Name = "RigaVenueB", City = cities[2], Address = "RigaVenueBAddres", Status = VenueStatuses.Avaliable},
                new Venue {Name = "WasawVenueA", City = cities[3], Address = "WasawVenueAAddres", Status = VenueStatuses.Avaliable},
                new Venue {Name = "WasawVenueB", City = cities[3], Address = "WasawVenueBAddres", Status = VenueStatuses.Avaliable},
                new Venue {Name = "PragaVenueA", City = cities[4], Address = "PragaVenueAAddres", Status = VenueStatuses.Avaliable},
                new Venue {Name = "PragaVenueB", City = cities[4], Address = "PragaVenueBAddres", Status = VenueStatuses.Avaliable},
                new Venue {Name = "BratislavaVenueA", City = cities[5], Address = "BratislavaVenueAAddres", Status = VenueStatuses.Avaliable},
                new Venue {Name = "BratislavaVenueB", City = cities[5], Address = "BratislavaVenueBAddres", Status = VenueStatuses.Avaliable}
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
                new Event {Category = categories[0], Name = "Concert3", Status = EventStatuses.Current, Venue = venues[3], Banner = "ImagineD3.jpg",
                    Description = "<div class=\"text-break\"> Concert3 description </></div>", Date = new DateTime(2022,03,03)},
                new Event {Category = categories[0], Name = "Concert4", Status = EventStatuses.Current, Venue = venues[4], Banner = "ImagineD4.jpg",
                    Description = "<div class=\"text-break\"> Concert4 description </></div>", Date = new DateTime(2022,04,04)},
                new Event {Category = categories[1], Name = "Sports1", Venue = venues[4], Status = EventStatuses.Current, Banner = "Foot1.jpg",
                    Description = "<div class=\"text-break\"> Sports1 description </></div>", Date = new DateTime(2022,05,05)},
                new Event {Category = categories[1], Name = "Sports2", Venue = venues[10], Status = EventStatuses.Current, Banner = "Foot2.jpg",
                    Description = "<div class=\"text-break\"> Sports2 description </></div>", Date = new DateTime(2022,06,06)},
                new Event {Category = categories[2], Name = "Exhibition1", Venue = venues[11], Status = EventStatuses.Current, Banner = "Exhibition1.jpg",
                    Description = "<div class=\"text-break\"> Exhibition1 description </></div>", Date = new DateTime(2022,07,07)},
            };

            users = new List<User>
            {
                new User {FirstName = "Harry", SecondName="Potter", Localization = "England", Address = "Address1",
                    PhoneNumber="111-11-11", UserName = "harry@hogwarts.en", Email = "harry@hogwarts.en", EmailConfirmed = true},

                new User {FirstName = "Ron", SecondName="Weasley", Localization = "England", Address = "Address2",
                    PhoneNumber="222-22-22", UserName = "ron@hogwarts.en", Email = "ron@hogwarts.en", EmailConfirmed = true},

                new User {FirstName = "Hermione", SecondName ="Granger", Localization = "England", Address = "Address3",
                    PhoneNumber="333-33-33", UserName = "hermione@hogwarts.en", Email = "hermione@hogwarts.en", EmailConfirmed = true},

                new User {FirstName = "Severus", SecondName ="Snape", Localization = "England", Address = "Address4",
                    PhoneNumber="444-44-44", UserName = "severus@hogwarts.en", Email = "severus@hogwarts.en", EmailConfirmed = true},
            };

            userPasswords = new string[] { "harry123", "ron123", "hermione123", "severus123" };

            administrators = new List<User>
            {
                new User {FirstName = "Albus", SecondName = "Dumbledore", Localization = "England", Address = "Hogwarts",
                    PhoneNumber = "555-55-55", UserName = "albus@hogwarts.en", Email = "albus@hogwarts.en", EmailConfirmed = true}
            };

            adminPasswords = new string[] { "albus123" };

            listings = new List<Listing> {
                new Listing {Name = "Severus listing 1", Event = events[5]}
            };

            tickets = new List<Ticket>
            {
                new Ticket {Event = events[0], Status = TicketStatuses.Selling, Price = 110, Seller = users[0], SellerNotes="Notes" },
                new Ticket {Event = events[0], Status = TicketStatuses.Selling, Price = 100, Seller = users[0], SellerNotes="Notes" },
                new Ticket {Event = events[0], Status = TicketStatuses.Selling, Price = 110, Seller = users[0], SellerNotes="Notes" },
                new Ticket {Event = events[1], Status = TicketStatuses.Selling, Price = 100, Seller = users[1], SellerNotes="Notes" },
                new Ticket {Event = events[2], Status = TicketStatuses.Selling, Price = 90, Seller = users[1], SellerNotes="Notes" },
                new Ticket {Event = events[3], Status = TicketStatuses.Selling, Price = 90, Seller = users[1], SellerNotes="Notes" },
                new Ticket {Event = events[5], Status = TicketStatuses.Selling, Price = 90, Seller = users[1], SellerNotes="Notes" },
                new Ticket {Event = events[5], Status = TicketStatuses.Selling, Price = 90, Seller = users[3], SellerNotes="Notes", Listing = listings[0] },
                new Ticket {Event = events[5], Status = TicketStatuses.Selling, Price = 90, Seller = users[3], SellerNotes="Notes", Listing = listings[0] },
            };
        }

        public async Task SeedDataAsync()
        {
            //await context.Database.EnsureDeletedAsync();
            //await context.Database.EnsureCreatedAsync();

            if (await context.Database.CanConnectAsync())
            {
                if (await roleManager.FindByNameAsync(UserRoles.Administrator) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = UserRoles.Administrator });
                }

                if (await roleManager.FindByNameAsync(UserRoles.User) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = UserRoles.User });
                }

                if (await roleManager.FindByNameAsync(UserRoles.Broker) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = UserRoles.Broker });
                }

                if ((await userManager.GetUsersInRoleAsync(UserRoles.Administrator)).Count() == 0)
                {
                    for (int i = 0; i < administrators.Count(); i++)
                    {
                        IdentityResult result = await userManager.CreateAsync(administrators[i], adminPasswords[i]);
                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(administrators[i], UserRoles.Administrator);
                        }
                    }
                }

                if ((await userManager.GetUsersInRoleAsync(UserRoles.User)).Count() == 0)
                {
                    for (int i = 0; i< users.Count(); i++)
                    {
                        IdentityResult result = await userManager.CreateAsync(users[i], userPasswords[i]);
                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(users[i], UserRoles.User);
                        }
                    }
                }

                if (!context.Categories.Any())
                {
                    await context.Categories.AddRangeAsync(categories);
                }

                if (!context.Events.Any())
                {
                    await context.Events.AddRangeAsync(events);
                }

                if (!context.Tickets.Any())
                {
                    await context.Tickets.AddRangeAsync(tickets);
                }

                if (!context.Listings.Any())
                {
                    await context.Listings.AddRangeAsync(listings);
                }

                if (!context.Cities.Any())
                {
                    await context.Cities.AddRangeAsync(cities);
                }

                if (!context.Venues.Any())
                {
                    await context.Venues.AddRangeAsync(venues);
                }

                await context.SaveChangesAsync();
            }
            else
            {
                throw new Exception(AppConstants.NoDBConnection);
            }
        }
    }
}
