using System;
using System.Collections.Generic;
using System.Linq;
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
    [Authorize(Roles = UserRoles.User)]
    public class TicketsController : Controller
    {
        private readonly ITicketsService ticketsService;
        private readonly IEventsService eventsService;
        private readonly IOrdersService ordersService;
        private readonly UserManager<User> userManager;
        private readonly IStringLocalizer<EventsController> stringLocalizer;

        public TicketsController(
            ITicketsService ticketsService,
            IEventsService eventsService,
            IOrdersService ordersService,
            UserManager<User> userManager,
            IStringLocalizer<EventsController> stringLocalizer)
        {
            this.ticketsService = ticketsService;
            this.ordersService = ordersService;
            this.eventsService = eventsService;
            this.userManager = userManager;
            this.stringLocalizer = stringLocalizer;
        }

        public async Task<IActionResult> Index()
        {
            var currentUserId = userManager.GetUserId(User);
            var userTickets = await ticketsService.GetTicketsByUserId(currentUserId);
            var myTickets = new List<TicketView> { };

            foreach (var ticket in userTickets)
            {
                var ticketEvent = await eventsService.GetEventById(ticket.EventId);
                var ticketOrder = await ordersService.GetOrderByTicketIdAndStatuses(ticket.Id, OrderStatuses.WaitingForConfirmation, OrderStatuses.Confirmed, OrderStatuses.Completed);
                var orderUser = await userManager.FindByIdAsync(ticketOrder?.UserId);

                myTickets.Add(
                    new TicketView
                    {
                        TicketId = ticket.Id,
                        TicketPrice = ticket.Price,
                        TicketStatus = ticket.Status,
                        BuyerName = orderUser?.UserName,
                        BuyerId = orderUser?.Id,
                        OrderId = ticketOrder?.Id,
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
                SellerId = userManager.GetUserId(User),
                Price = ticketModel.Price,
                SellerNotes = ticketModel.Notes,
                Status = TicketStatuses.Selling
            };

            await ticketsService.AddTicket(ticket);

            return View("InstructionTicketAdded");
        }

        public async Task<IActionResult> RemoveTicket(int ticketId)
        {
            var ticket = await ticketsService.GetTicketById(ticketId);
            await ticketsService.ChangeTicketStatus(ticket, TicketStatuses.Removed);

            return View("InstructionTicketDeleted");
        }

        public async Task<IActionResult> ConfirmOrder(int ticketId)
        {
            var ticket = await ticketsService.GetTicketById(ticketId);
            var order = await ordersService.GetOrderByTicketIdAndStatuses(ticket.Id, OrderStatuses.WaitingForConfirmation);

            await ticketsService.ChangeTicketStatus(ticket, TicketStatuses.WaitingForReceivingConfirmation);
            await ordersService.ChangeOrderStatus(order, OrderStatuses.Confirmed);

            return View("InstructionOrderConfirmed");
        }

        public async Task<IActionResult> RejectOrder(int ticketId)
        {
            var ticket = await ticketsService.GetTicketById(ticketId);
            var order = await ordersService.GetOrderByTicketIdAndStatuses(ticket.Id, OrderStatuses.WaitingForConfirmation);

            await ticketsService.ChangeTicketStatus(ticket, TicketStatuses.Selling);
            await ordersService.ChangeOrderStatus(order, OrderStatuses.Rejected);

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
            var order = await ordersService.GetOrderByTicketIdAndStatuses(ticket.Id, OrderStatuses.Confirmed);

            await ordersService.ChangeOrderTracking(order, tracking.TrackingNumber);

            return View("InstructionTrackingAdded");
        }
    }
}