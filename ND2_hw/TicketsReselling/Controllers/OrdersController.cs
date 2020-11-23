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
    public class OrdersController : Controller
    {
        private readonly ITicketsService ticketsService;
        private readonly IEventsService eventsService;
        private readonly IOrdersService ordersService;
        private readonly UserManager<User> userManager;
        private readonly IStringLocalizer<EventsController> stringLocalizer;

        public OrdersController(
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
            var userOrders = await ordersService.GetOrdersByUserId(currentUserId);
            var myOrders = new List<OrderView> { };

            foreach (var order in userOrders)
            {
                var orderTicket = await ticketsService.GetTicketById(order.TicketId);
                var ticketEvent = await eventsService.GetEventById(orderTicket.EventId);

                myOrders.Add(
                    new OrderView
                    {
                        OrderId = order.Id,
                        TicketPrice = orderTicket.Price,
                        OrderStatus = order.Status,
                        SellerName = (await userManager.FindByIdAsync(orderTicket.SellerId))?.UserName,
                        SellerId = orderTicket.SellerId,
                        EventId = orderTicket.EventId,
                        EventName = ticketEvent.Name,
                        EventDate = ticketEvent.Date,
                    }
                );
            };

            var model = new OrderViewModel
            {
                Orders = myOrders.ToArray()
            };

            return View(model);
        }

        public async Task<IActionResult> AddOrder(int ticketId)
        {
            var ticket = await ticketsService.GetTicketById(ticketId);
            await ticketsService.ChangeTicketStatus(ticket, TicketStatuses.WaitingForConfirmation);

            await ordersService.AddOrder(new Order
            {
                TicketId = ticket.Id,
                Status = (int)OrderStatuses.WaitingForConfirmation,
                UserId = userManager.GetUserId (User)
            });

            return View("InstructionOrderAdded");
        }

        public async Task<IActionResult> CancelOrder(int orderId)
        {
            var order = await ordersService.GetOrderById(orderId);
            var ticket = await ticketsService.GetTicketById(order.TicketId);

            await ticketsService.ChangeTicketStatus(ticket, TicketStatuses.Selling);
            await ordersService.ChangeOrderStatus(order, OrderStatuses.Cancelled);

            return View("InstructionOrderCanceled");
        }

        public async Task<IActionResult> TrackingInfo(int orderId)
        {
            ViewBag.trackingNumber = (await ordersService.GetOrderById(orderId))?.TrackingNumber;

            return View("TrackingNumberInfo");
        }

        public async Task<IActionResult> ConfirmOrderReceiving(int orderId)
        {
            var order = await ordersService.GetOrderById(orderId);
            var ticket = await ticketsService.GetTicketById(order.TicketId);

            await ordersService.ChangeOrderStatus(order, OrderStatuses.Completed);
            await ticketsService.ChangeTicketStatus(ticket, TicketStatuses.Sold);

            return View("InstructionOrderReceivingConfirmed");
        }

        public async Task<IActionResult> RemoveOrder(int orderId)
        {
            var order = await ordersService.GetOrderById(orderId);

            await ordersService.ChangeOrderStatus(order, OrderStatuses.Removed);

            return View("InstructionOrderRemoved");
        }
    }
}