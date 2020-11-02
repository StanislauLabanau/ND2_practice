using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsReselling.Business.Enums;
using TicketsReselling.Business.Models;
using TicketsReselling.DAL;
using TicketsReselling.DAL.Models;

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
        private readonly string[] userPasswords;
        private readonly string[] adminPasswords;

        public DataSeeder(TicketsResellingContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.context = context;

            cities = new List<City>
            {
                new City {Name = "Tallin"},
                new City {Name = "Vilnius"},
                new City {Name = "Riga"},
                new City {Name = "Warsaw"},
                new City {Name = "Praga"},
                new City {Name = "Bratislava"},
            };

            venues = new List<Venue>
            {
                new Venue {Name = "TallinVenueA", City = cities[0], Address = "TallinVenueAAddres"},
                new Venue {Name = "TallinVenueB", City = cities[0], Address = "TallinVenueBAddres"},
                new Venue {Name = "VilniusVenueA", City = cities[1], Address = "VilniusVenueAAddres"},
                new Venue {Name = "VilniusVenueB", City = cities[1], Address = "VilniusVenueBAddres"},
                new Venue {Name = "RigaVenueA", City = cities[2], Address = "RigaVenueAAddres"},
                new Venue {Name = "RigaVenueB", City = cities[2], Address = "RigaVenueBAddres"},
                new Venue {Name = "WasawVenueA", City = cities[3], Address = "WasawVenueAAddres"},
                new Venue {Name = "WasawVenueB", City = cities[3], Address = "WasawVenueBAddres"},
                new Venue {Name = "PragaVenueA", City = cities[4], Address = "PragaVenueAAddres"},
                new Venue {Name = "PragaVenueB", City = cities[4], Address = "PragaVenueBAddres"},
                new Venue {Name = "BratislavaVenueA", City = cities[5], Address = "BratislavaVenueAAddres"},
                new Venue {Name = "BratislavaVenueB", City = cities[5], Address = "BratislavaVenueBAddres"}
            };

            categories = new List<Category>
            {
                new Category {Name = "All categories"},
                new Category {Name = "Concerts"},
                new Category {Name = "Sports"},
                new Category {Name = "Exhibitions"}
            };

            events = new List<Event>
            {
                new Event {Category = categories[1], Name = "Concert1", Status = (int) EventStatuses.Current, Venue = venues[0], Banner = "ImagineD1.jpg",
                    Description = "<div class=\"text-break\"> Concert1 description </></div>", Date = new DateTime(2022,01,01)},
                new Event {Category = categories[1], Name = "Concert2", Status = (int) EventStatuses.Current, Venue = venues[1], Banner = "ImagineD2.jpg",
                    Description = "<div class=\"text-break\"> Concert2 description </></div>", Date = new DateTime(2022,02,02)},
                new Event {Category = categories[1], Name = "Concert3", Status = (int) EventStatuses.Current, Venue = venues[3], Banner = "ImagineD3.jpg",
                    Description = "<div class=\"text-break\"> Concert3 description </></div>", Date = new DateTime(2022,03,03)},
                new Event {Category = categories[1], Name = "Concert4", Status = (int) EventStatuses.Current, Venue = venues[4], Banner = "ImagineD4.jpg",
                    Description = "<div class=\"text-break\"> Concert4 description </></div>", Date = new DateTime(2022,04,04)},
                new Event {Category = categories[2], Name = "Sports1", Venue = venues[4], Status = (int) EventStatuses.Current, Banner = "Foot1.jpg",
                    Description = "<div class=\"text-break\"> Sports1 description </></div>", Date = new DateTime(2022,05,05)},
                new Event {Category = categories[2], Name = "Sports2", Venue = venues[10], Status = (int) EventStatuses.Current, Banner = "Foot2.jpg",
                    Description = "<div class=\"text-break\"> Sports2 description </></div>", Date = new DateTime(2022,06,06)},
                new Event {Category = categories[3], Name = "Exhibition1", Venue = venues[11], Status = (int) EventStatuses.Current, Banner = "Exhibition1.jpg",
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

                //new User {FirstName = "Tom", SecondName ="Riddle", Localization = "England", Address = "Address4",
                //    PhoneNumber="444-44-44", UserName = "Tom", Email = "tom@hogwarts.en"},
            };

            userPasswords = new string[] { "harry123", "ron123", "hermione123" };

            administrators = new List<User>
            {
                new User {FirstName = "Albus", SecondName = "Dumbledore", Localization = "England", Address = "Hogwarts",
                    PhoneNumber = "555-55-55", UserName = "albus@hogwarts.en", Email = "albus@hogwarts.en", EmailConfirmed = true}
            };

            adminPasswords = new string[] { "albus123" };

            tickets = new List<Ticket>
            {
                new Ticket {EventId = 1, Status = (int) TicketStatuses.Selling, Price = 110, Seller = users[0], SellerNotes="Notes" },
                new Ticket {EventId = 1, Status = (int) TicketStatuses.Selling, Price = 100, Seller = users[0], SellerNotes="Notes" },
                new Ticket {EventId = 1, Status = (int) TicketStatuses.Selling, Price = 110, Seller = users[0], SellerNotes="Notes" },
                new Ticket {EventId = 2, Status = (int) TicketStatuses.Selling, Price = 100, Seller = users[1], SellerNotes="Notes" },
                new Ticket {EventId = 3, Status = (int) TicketStatuses.Selling, Price = 90, Seller = users[1], SellerNotes="Notes" },
                new Ticket {EventId = 4, Status = (int) TicketStatuses.Selling, Price = 90, Seller = users[1], SellerNotes="Notes" },
                new Ticket {EventId = 5, Status = (int) TicketStatuses.Selling, Price = 90, Seller = users[1], SellerNotes="Notes" },
            };
        }

        public async Task SeedDataAsync()
        {
            context.Database.EnsureDeleted();

            context.Database.EnsureCreated();

            if (await roleManager.FindByNameAsync(UserRoles.Administrator) == null)
            {
                await roleManager.CreateAsync(new IdentityRole { Name = UserRoles.Administrator });
            }

            if (await roleManager.FindByNameAsync(UserRoles.User) == null)
            {
                await roleManager.CreateAsync(new IdentityRole { Name = UserRoles.User });
            }

            if ((await userManager.GetUsersInRoleAsync(UserRoles.Administrator)).Count() == 0)
            {
                for (int i = 0; i < administrators.Count(); i++)
                {
                    IdentityResult result = await userManager.CreateAsync(administrators[i], adminPasswords[i]);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(administrators[i], UserRoles.Administrator);
                        await userManager.AddToRoleAsync(administrators[i], UserRoles.User);
                    }
                }
            }

            if ((await userManager.GetUsersInRoleAsync(UserRoles.User)).Count() == 0)
            {
                for (int i = 0; i < users.Count(); i++)
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
    }
}
