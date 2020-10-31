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
    public class OrdersController : Controller
    {
        private readonly TicketsService ticketsService;
        private readonly EventsService eventsService;
        private readonly OrdersService ordersService;
        private readonly UsersRepository usersRepository;
        private readonly IStringLocalizer<EventsController> stringLocalizer;


        public OrdersController(
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
            var userOrders = await ordersService.GetOrdersByUserId(currentUser.Id);

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
                        SellerName = usersRepository.GetUserById(orderTicket.SellerId)?.UserName,
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
            await ticketsService.ChangeTicketStatus(ticket, (int)TicketStatuses.WaitingForConfirmation);

            await ordersService.AddOrder(new Order
            {
                TicketId = ticket.Id,
                Status = (int)OrderStatuses.WaitingForConfirmation,
                UserId = usersRepository.GetUserByUserName(User.Identity.Name).Id,
            });

            return View("InstructionOrderAdded");
        }

        public async Task<IActionResult> CancelOrder(int orderId)
        {
            var order = await ordersService.GetOrerById(orderId);
            var ticket = await ticketsService.GetTicketById(order.TicketId);

            await ticketsService.ChangeTicketStatus(ticket, (int)TicketStatuses.Selling);
            await ordersService.ChangeOrderStatus(order, (int) OrderStatuses.Cancelled);

            return View("InstructionOrderCanceled");
        }

        public async Task<IActionResult> TrackingInfo(int orderId)
        {
            ViewBag.trackingNumber = (await ordersService.GetOrerById(orderId))?.TrackingNumber;

            return View("TrackingNumberInfo");
        }

        public async Task<IActionResult> ConfirmOrderReceiving(int orderId)
        {
            var order = await ordersService.GetOrerById(orderId);
            var ticket = await ticketsService.GetTicketById(order.TicketId);

            await ordersService.ChangeOrderStatus(order, (int) OrderStatuses.Completed);
            await ticketsService.ChangeTicketStatus(ticket, (int)TicketStatuses.Sold);


            return View("InstructionOrderReceivingConfirmed");
        }

        public async Task<IActionResult> RemoveOrder(int orderId)
        {
            var order = await ordersService.GetOrerById(orderId);

            await ordersService.ChangeOrderStatus(order, (int)OrderStatuses.Removed);

            return View("InstructionOrderRemoved");
        }
    }
}