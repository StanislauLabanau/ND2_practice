using System;
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
    [Authorize(Roles = UserRoles.User)]
    public class TicketsController : Controller
    {
        private readonly TicketsService ticketsService;
        private readonly EventsService eventsService;
        private readonly OrdersService ordersService;
        private readonly UsersRepository usersRepository;
        private readonly IStringLocalizer<EventsController> stringLocalizer;


        public TicketsController(
            TicketsService ticketsService,
            EventsService eventsService,
            OrdersService ordersService,
            UsersRepository usersRepository,
            IStringLocalizer<EventsController> stringLocalizer)
        {
            this.ticketsService = ticketsService;
            this.ordersService = ordersService;
            this.eventsService = eventsService;
            this.usersRepository = usersRepository;
            this.stringLocalizer = stringLocalizer;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = usersRepository.GetUserByUserName(User.Identity.Name);
            var userTickets = await ticketsService.GetTicketsByUserId(currentUser.Id);

            var myTickets = new List<TicketView> { };

            foreach (var ticket in userTickets)
            {
                var ticketEvent = await eventsService.GetEventById(ticket.EventId);
                var ticketOrder = await ordersService.GetOrderByTicketIdAndStatus(ticket.Id,
                    (int) OrderStatuses.WaitingForConfirmation,
                    (int) OrderStatuses.Confirmed,
                    (int) OrderStatuses.Completed);
                int buyerId = 0;
                int orderId = 0;
                string buyerName = null;

                if(ticketOrder!=null)
                {
                    buyerId = ticketOrder.UserId;
                    orderId = ticketOrder.Id;
                    buyerName = usersRepository.GetUserById(buyerId).UserName;
                }

                myTickets.Add(
                    new TicketView
                    {
                        TicketId = ticket.Id,
                        TicketPrice = ticket.Price,
                        TicketStatus = ticket.Status,
                        BuyerName = buyerName,
                        BuyerId = buyerId,
                        OrderId = orderId,
                        EventName = ticketEvent.Name,
                        EventId = ticket.EventId,
                        EventDate = ticketEvent.Date,
                        OrderTrackingNumber = ticketOrder?.TrackingNumber
                    }
                );
            };

            var model = new TicketsViewModel
            {
                Tickets = myTickets.ToArray()
            };

            return View(model);
        }

        public async Task<IActionResult> AddTicket(int eventId)
        {
            ViewBag.EventId = eventId;
            ViewBag.EventName = (await eventsService.GetEventById(eventId))?.Name;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTicket(AddTicketViewModel ticketModel, int eventId)
        {
            var ticket = new Ticket
            {
                EventId = eventId,
                SellerId = usersRepository.GetUserByUserName(User.Identity.Name).Id,
                Price = ticketModel.Price,
                SellerNotes = ticketModel.Notes,
                Status = (int)TicketStatuses.Selling
            };

            await ticketsService.AddTicket(ticket);

            return View("InstructionTicketAdded");
        }

        public async Task<IActionResult> RemoveTicket(int ticketId)
        {
            var ticket = await ticketsService.GetTicketById(ticketId);
            await ticketsService.ChangeTicketStatus(ticket, (int) TicketStatuses.Removed);

            return View("InstructionTicketDeleted");
        }

        public async Task<IActionResult> ConfirmOrder(int ticketId)
        {
            var ticket = await ticketsService.GetTicketById(ticketId);
            var order = await ordersService.GetOrderByTicketIdAndStatus(ticket.Id, (int) OrderStatuses.WaitingForConfirmation);

            await ticketsService.ChangeTicketStatus(ticket, (int)TicketStatuses.WaitingForReceivingConfirmation);
            await ordersService.ChangeOrderStatus(order, (int)OrderStatuses.Confirmed);

            return View("InstructionOrderConfirmed");
        }

        public async Task<IActionResult> RejectOrder(int ticketId)
        {
            var ticket = await ticketsService.GetTicketById(ticketId);
            var order = await ordersService.GetOrderByTicketIdAndStatus(ticket.Id, (int) OrderStatuses.WaitingForConfirmation);

            await ticketsService.ChangeTicketStatus(ticket, (int)TicketStatuses.Selling);
            await ordersService.ChangeOrderStatus(order, (int)OrderStatuses.Rejected);

            return View("InstructionOrderRejected");
        }

        public IActionResult AddTracking(int ticketId)
        {
            ViewBag.ticketId = ticketId;
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddTracking(AddTrackingViewModel tracking)
        {
            var ticket = await ticketsService.GetTicketById(tracking.TicketId);
            var order = await ordersService.GetOrderByTicketIdAndStatus(ticket.Id, (int) OrderStatuses.Confirmed);
            await ordersService.ChangeOrderTracking(order, tracking.TrackingNumber);

            return View("InstructionTrackingAdded");
        }
    }
}