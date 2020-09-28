using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketsReselling.Business;
using TicketsReselling.Business.Enums;
using TicketsReselling.Business.Models;
using TicketsReselling.Models;
using static TicketsReselling.Models.EventWithTicketsViewModel;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TicketsReselling.Controllers
{
    public class EventsController : Controller
    {
        private readonly TicketsRepository ticketsRepository;
        private readonly UsersRepository usersRepository;
        private readonly EventsRepository eventsRepository;
        private readonly OrdersRepository orderRepository;

        public EventsController(TicketsRepository ticketsRepository, UsersRepository usersRepository, EventsRepository eventsRepository, OrdersRepository orderRepository)
        {
            this.ticketsRepository = ticketsRepository;
            this.usersRepository = usersRepository;
            this.eventsRepository = eventsRepository;
            this.orderRepository = orderRepository;
        }

        public IActionResult Index(int categoryId)
        {
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

        [Authorize(Roles ="Administrator")]

        public IActionResult AddEvent()
        {
            ViewBag.categories = eventsRepository.GetCategories();
            ViewBag.venues = eventsRepository.GetVenues();
            return View();
        }

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

