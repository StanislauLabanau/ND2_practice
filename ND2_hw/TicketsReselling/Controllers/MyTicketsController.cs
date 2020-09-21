using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketsReselling.Business;
using TicketsReselling.Models;

namespace TicketsReselling.Controllers
{
    public class MyTicketsController : Controller
    {
        private readonly TicketsRepository ticketsRepository;
        private readonly UsersRepository usersRepository;
        private readonly EventsRepository eventsRepository;

        public MyTicketsController(TicketsRepository ticketsRepository, UsersRepository usersRepository, EventsRepository eventsRepository)
        {
            this.ticketsRepository = ticketsRepository;
            this.usersRepository = usersRepository;
            this.eventsRepository = eventsRepository;
        }

        [Authorize(Roles = "User")]
        public IActionResult Index()
        {
            var currentUser = usersRepository.GetUserByUserName(User.Identity.Name);
            var userTickets = ticketsRepository.GetTickets().Where(t => t.SellerId == currentUser.Id);

            var myTickets = new List<MyTicket> { };

            foreach (var ticket in userTickets)
            {
                var ticketEvent = eventsRepository.GetEventById(ticket.EventId);

                myTickets.Add(
                    new MyTicket
                    {
                        TicketId = ticket.Id,
                        TicketPrice = ticket.Price,
                        TicketStatus = ticket.Status,
                        BuyerName = $"{usersRepository.GetUserById(ticket.BuyerId)?.FirstName} {usersRepository.GetUserById(ticket.BuyerId)?.SecondName}",
                        EventName = ticketEvent.Name,
                        EventDate = ticketEvent.Date,
                    }
                );
            };

            var model = new MyTicketsViewModel
            {
                Statuses = ticketsRepository.GetStatuses(),
                Tickets = myTickets.ToArray()
            };

            return View(model);
        }
    }
}