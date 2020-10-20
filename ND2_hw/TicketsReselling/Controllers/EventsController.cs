using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using TicketsReselling.Business;
using TicketsReselling.Business.Enums;
using TicketsReselling.Business.Models;
using TicketsReselling.Models;


namespace TicketsReselling.Controllers
{
    public class EventsController : Controller
    {
        private readonly TicketsRepository ticketsRepository;
        private readonly UsersRepository usersRepository;
        private readonly EventsRepository eventsRepository;
        private readonly OrdersRepository orderRepository;
        private readonly IStringLocalizer<EventsController> stringLocalizer;

        public EventsController(TicketsRepository ticketsRepository, UsersRepository usersRepository
            , EventsRepository eventsRepository, OrdersRepository orderRepository
            , IStringLocalizer<EventsController> stringLocalizer)
        {
            this.ticketsRepository = ticketsRepository;
            this.usersRepository = usersRepository;
            this.eventsRepository = eventsRepository;
            this.orderRepository = orderRepository;
            this.stringLocalizer = stringLocalizer;
        }

        public IActionResult Index(int categoryId)
        {
            ViewData["Title"] = stringLocalizer["eventpagetitle"];

            ViewBag.categoryId = categoryId;
            ViewBag.categoryName = eventsRepository.GetCategoryById(categoryId).Name;

            var model = new EventsViewModel
            {
                Categories = eventsRepository.GetCategories(),
                Events = eventsRepository.GetEvents()
            };

            return View(model);
        }

        public IActionResult EventWithTickets(int eventId)
        {
            var tickets = ticketsRepository.GetTickets();
            var currentEvent = eventsRepository.GetEventById(eventId);
            var eventTickets = tickets.Where(ticket => ticket.EventId == eventId && ticket.Status == TicketStatuses.Selling);
            var ticketsList = new List<EventTickets>();

            foreach (var ticket in eventTickets)
            {
                var seller = usersRepository.GetUserById(ticket.SellerId);
                ticketsList.Add(
                    new EventTickets
                    {
                        TicketId = ticket.Id,
                        SellerName = $"{seller?.FirstName} {seller?.SecondName}",
                        Price = ticket.Price,
                        SellerNotes = ticket.SellerNotes
                    }
                );
            };

            var model = new EventWithTicketsViewModel
            {
                Event = currentEvent,
                Tickets = ticketsList.ToArray()
            };

            return View(model);
        }

        [Authorize(Roles =UserRoles.Administrator)]
        public IActionResult AddEvent()
        {
            ViewBag.categories = eventsRepository.GetCategories();
            ViewBag.venues = eventsRepository.GetVenues();
            return View();
        }

        [Authorize(Roles = UserRoles.Administrator)]
        [HttpPost]
        public  IActionResult AddEvent(AddEventViewModel newEvent)
        {
            if (ModelState.IsValid)
            {
                var bannerName = FileUploader.UploadFile(newEvent.Banner).Result;

                eventsRepository.AddEvent(new Event
                { 
                    Id = ++EventsRepository.EventIdCounter,
                    Description = newEvent.Description,
                    Name = newEvent.Name,
                    Banner = bannerName,
                    Category = eventsRepository.GetCategoryById(newEvent.CategoryId),
                    Date = newEvent.Date,
                    Venue = eventsRepository.GetVenueById(newEvent.VenueId)
                });

                return View("InstructionEventAdded");
            }

            return View("Index");
        }
    }
}

