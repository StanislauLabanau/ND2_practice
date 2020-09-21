using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketsReselling.Business;
using TicketsReselling.Business.Models;
using TicketsReselling.Models;
using static TicketsReselling.Models.EventTicketsViewModel;

namespace TicketsReselling.Controllers
{
    public class EventTicketsController : Controller
    {
        private readonly TicketsRepository ticketsRepository;
        private readonly UsersRepository usersRepository;
        private readonly EventsRepository eventsRepository;

        public EventTicketsController(TicketsRepository ticketsRepository, UsersRepository usersRepository, EventsRepository eventsRepository)
        {
            this.ticketsRepository = ticketsRepository;
            this.usersRepository = usersRepository;
            this.eventsRepository = eventsRepository;
        }

        public IActionResult Index(int eventId)
        {
            var tickets = ticketsRepository.GetTickets();
            var currentEvent = eventsRepository.GetEventById(eventId);
            var eventTickets = tickets.Where(ticket => ticket.EventId == eventId && ticket.Status == ticketsRepository.GetStatuses()[1]);
            var ticketsList = new List<EventTickets>();

            foreach (var ticket in eventTickets)
            {
                var seller = usersRepository.GetUserById(ticket.SellerId);
                ticketsList.Add(
                    new EventTickets
                    {
                        Id = ticket.Id,
                        SellerName = $"{seller?.FirstName} {seller?.SecondName}",
                        Price = ticket.Price,
                        SellerNotes = ticket.SellerNotes
                    }
                );
            };

            var model = new EventTicketsViewModel
            {
                Event = currentEvent,
                Tickets = ticketsList.ToArray()
            };

            return View(model);
        }
    }
}