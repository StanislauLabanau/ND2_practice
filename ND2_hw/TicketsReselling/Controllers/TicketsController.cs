using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketsReselling.Business;
using TicketsReselling.Business.Enums;
using TicketsReselling.Business.Models;
using TicketsReselling.Models;

namespace TicketsReselling.Controllers
{
    [Authorize(Roles = "User")]
    public class TicketsController : Controller
    {
        private readonly TicketsRepository ticketsRepository;
        private readonly UsersRepository usersRepository;
        private readonly EventsRepository eventsRepository;
        private readonly OrdersRepository ordersRepository;

        public TicketsController(TicketsRepository ticketsRepository, UsersRepository usersRepository, EventsRepository eventsRepository, OrdersRepository ordersRepository)
        {
            this.ticketsRepository = ticketsRepository;
            this.usersRepository = usersRepository;
            this.eventsRepository = eventsRepository;
            this.ordersRepository = ordersRepository;
        }

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
                        BuyerId = ticket.BuyerId,
                        EventName = ticketEvent.Name,
                        EventId = ticket.EventId,
                        EventDate = ticketEvent.Date,
                    }
                );
            };

            var model = new TicketsViewModel
            {
                Tickets = myTickets.ToArray()
            };

            return View(model);
        }

        public IActionResult AddTicket(int eventId)
        {
            ViewBag.EventId = eventId;
            ViewBag.EventName = eventsRepository.GetEventById(eventId).Name;

            return View();
        }

        [HttpPost]
        public IActionResult AddTicket(AddTicketViewModel ticketModel, int eventId)
        {
            var ticket = new Ticket
            {
                Id = ++TicketsRepository.IdCounter,
                EventId = eventId,
                SellerId = usersRepository.GetUserByUserName(User.Identity.Name).Id,
                Price = ticketModel.Price,
                SellerNotes = ticketModel.Notes,
                Status = TicketStatuses.Selling
            };

            ticketsRepository.AddTicket(ticket);

            return View("InstructionTicketAdded");
        }

        public IActionResult RemoveTicket(int ticketId)
        {
            ticketsRepository.RemoveTicket(ticketId);

            return View("InstructionTicketDeleted");
        }

        public IActionResult ConfirmOrder(int ticketId)
        {
            var ticket = ticketsRepository.GetTicket(ticketId);
            var order = ordersRepository.GetOrderByTicketId(ticketId);

            ticket.Status = TicketStatuses.Sold;
            order.Status = OrderStatuses.Confirmed;

            ticketsRepository.RemoveTicket(ticketId);
            ticketsRepository.AddTicket(ticket);
            ordersRepository.DeleteOrder(order.Id);
            ordersRepository.AddOrder(order);

            return View("InstructionOrderConfirmed");
        }

        public IActionResult RejectOrder(int ticketId)
        {
            var ticket = ticketsRepository.GetTicket(ticketId);
            var order = ordersRepository.GetOrderByTicketId(ticketId);

            ticket.Status = TicketStatuses.Selling;
            order.Status = OrderStatuses.Rejected;

            ticketsRepository.RemoveTicket(ticketId);
            ticketsRepository.AddTicket(ticket);
            ordersRepository.DeleteOrder(order.Id);
            ordersRepository.AddOrder(order);

            return View("InstructionOrderRejected");
        }
    }
}