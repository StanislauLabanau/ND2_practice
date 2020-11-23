using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using TicketsReselling.DAL.Enums;
using TicketsReselling.Business.Models;
using TicketsReselling.Core;
using TicketsReselling.DAL.Models;
using TicketsReselling.Models;
using TicketsReselling.Core.Interfaces;

namespace TicketsReselling.Controllers
{
    public class EventsController : Controller
    {
        private readonly ITicketsService ticketsService;
        private readonly IEventsService eventsService;
        private readonly IVenuesService venuesService;
        private readonly ICitiesService citiesService;
        private readonly UserManager<User> userManager;
        private readonly IStringLocalizer<EventsController> stringLocalizer;

        public EventsController(
            ITicketsService ticketsService,
            IEventsService eventsService,
            IVenuesService venuesService,
            ICitiesService citiesService,
            UserManager<User> userManager,
            IStringLocalizer<EventsController> stringLocalizer)
        {
            this.ticketsService = ticketsService;
            this.venuesService = venuesService;
            this.citiesService = citiesService;
            this.eventsService = eventsService;
            this.userManager = userManager;
            this.stringLocalizer = stringLocalizer;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = stringLocalizer["eventpagetitle"];

            var model = new EventsViewModel
            {
                Categories = await eventsService.GetCategories(),
                Cities = await citiesService.GetCityesByStatus(CityStatuses.Avaliable)
            };

            return View(model);
        }

        public async Task<IActionResult> EventWithTickets(int eventId)
        {
            var currentEvent = await eventsService.GetEventById(eventId);
            var eventTickets = await ticketsService.GetTicketsByEventIdAndStatus(eventId, TicketStatuses.Selling);
            currentEvent.Venue = await venuesService.GetVenueById(currentEvent.VenueId);
            currentEvent.Venue.City = await citiesService.GetCityById(currentEvent.Venue.CityId);
            var ticketsList = new List<EventTickets>();

            foreach (var ticket in eventTickets)
            {
                var seller = await userManager.FindByIdAsync(ticket.SellerId);

                ticketsList.Add(
                    new EventTickets
                    {
                        TicketId = ticket.Id,
                        SellerName = seller.UserName,
                        Price = ticket.Price,
                        SellerNotes = ticket.SellerNotes
                    }
                );
            };

            var model = new EventWithTicketsViewModel
            {
                Event = currentEvent,
                Tickets = ticketsList
            };

            return View(model);
        }

        [Authorize(Roles = UserRoles.Administrator)]
        public async Task<IActionResult> AddEvent()
        {
            ViewBag.categories = await eventsService.GetCategories();
            ViewBag.venues = await venuesService.GetVenuesByStatuses(VenueStatuses.Avaliable);
            return View();
        }

        [Authorize(Roles = UserRoles.Administrator)]
        [HttpPost]
        public async Task<IActionResult> AddEvent(AddEventViewModel newEvent)
        {
            if (ModelState.IsValid)
            {
                var bannerName = FileUploader.UploadFile(newEvent.Banner).Result;

                await eventsService.AddEvent(new Event
                {
                    Description = newEvent.Description,
                    Name = newEvent.Name,
                    Banner = bannerName,
                    Category = await eventsService.GetCategoryById(newEvent.CategoryId),
                    Date = newEvent.Date,
                    Venue = await venuesService.GetVenueById(newEvent.VenueId)
                });

                return View("InstructionEventAdded");
            }

            return View("Index");
        }

        [Authorize(Roles = UserRoles.Administrator)]
        public async Task<IActionResult> RemoveEvent(int eventId)
        {
            var eventItem = await eventsService.GetEventById(eventId);
            await eventsService.ChangeEventStatus(eventItem, EventStatuses.Removed);

            return View("InstructionEventRemoved");
        }
    }
}

