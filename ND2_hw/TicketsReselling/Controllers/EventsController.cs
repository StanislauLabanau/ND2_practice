using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using TicketsReselling.Business;
using TicketsReselling.Business.Enums;
using TicketsReselling.Business.Models;
using TicketsReselling.Core;
using TicketsReselling.DAL.Models;
using TicketsReselling.Models;


namespace TicketsReselling.Controllers
{
    public class EventsController : Controller
    {
        private readonly TicketsService ticketsService;
        private readonly EventsService eventsService;
        private readonly VenuesService venuesService;
        private readonly CitiesService citiesService;
        private readonly UsersRepository usersRepository;
        private readonly IStringLocalizer<EventsController> stringLocalizer;


        public EventsController(
            TicketsService ticketsService,
            EventsService eventsService,
            VenuesService venuesService,
            CitiesService citiesService,
            UsersRepository usersRepository,
            IStringLocalizer<EventsController> stringLocalizer)
        {
            this.ticketsService = ticketsService;
            this.venuesService = venuesService;
            this.citiesService = citiesService;
            this.eventsService = eventsService;
            this.usersRepository = usersRepository;
            this.stringLocalizer = stringLocalizer;
        }


        public async Task<IActionResult> Index(int categoryId=1)
        {
            ViewData["Title"] = stringLocalizer["eventpagetitle"];

            ViewBag.categoryId = categoryId;
            ViewBag.categoryName = (await eventsService.GetCategoryById(categoryId))?.Name;

            var model = new EventsViewModel
            {
                Categories = await eventsService.GetCategories(),
                Events = await eventsService.GetEventsByStatus((int) EventStatuses.Current)
            };

            foreach (Event eventItem in model.Events)
            {
                eventItem.Venue = await venuesService.GetVenueById(eventItem.VenueId);
                eventItem.Venue.City = await citiesService.GetCityById(eventItem.Venue.CityId);
            }

            return View(model);
        }

        public async Task<IActionResult> EventWithTickets(int eventId)
        {
            var currentEvent = await eventsService.GetEventById(eventId);
            var eventTickets = await ticketsService.GetTicketsByEventIdAndStatus(eventId, (int) TicketStatuses.Selling);
            currentEvent.Venue = await venuesService.GetVenueById(currentEvent.VenueId);
            currentEvent.Venue.City = await citiesService.GetCityById(currentEvent.Venue.CityId);
            var ticketsList = new List<EventTickets>();

            foreach (var ticket in eventTickets)
            {
                var seller = usersRepository.GetUserById(ticket.SellerId);
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
            ViewBag.venues = await venuesService.GetVenues();
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
            await eventsService.ChangeEventStatus(eventItem, (int) EventStatuses.Removed);

            return View("InstructionEventRemoved");
        }
    }
}

