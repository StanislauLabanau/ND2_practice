using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketsReselling.Business;
using TicketsReselling.Business.Enums;
using TicketsReselling.Business.Models;
using TicketsReselling.Models;

namespace TicketsReselling.Controllers
{
    [Authorize(Roles = "User")]
    public class OrdersController : Controller
    {
        private readonly TicketsRepository ticketsRepository;
        private readonly UsersRepository usersRepository;
        private readonly EventsRepository eventsRepository;
        private readonly OrdersRepository orderRepository;

        public OrdersController(TicketsRepository ticketsRepository, UsersRepository usersRepository, EventsRepository eventsRepository, OrdersRepository orderRepository)
        {
            this.ticketsRepository = ticketsRepository;
            this.usersRepository = usersRepository;
            this.eventsRepository = eventsRepository;
            this.orderRepository = orderRepository;
        }

        public IActionResult Index()
        {
            var currentUser = usersRepository.GetUserByUserName(User.Identity.Name);
            var userOrders = orderRepository.GetOrders().Where(o => o.UserId == currentUser.Id);

            var myOrders = new List<MyOrder> { };

            foreach (var order in userOrders)
            {
                var orderTicket = ticketsRepository.GetTicket(order.TicketId);
                var ticketEvent = eventsRepository.GetEventById(orderTicket.EventId);

                myOrders.Add(
                    new MyOrder
                    {
                        OrderId = order.Id,
                        TicketPrice = orderTicket.Price,
                        OrderStatus = order.Status,
                        SellerName = $"{usersRepository.GetUserById(orderTicket.SellerId)?.FirstName} {usersRepository.GetUserById(orderTicket.SellerId)?.SecondName}",
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

        public IActionResult AddOrder(int ticketId)
        {
            var ticket = ticketsRepository.GetTicket(ticketId);

            ticket.Status = TicketStatuses.WaitingForConfirmation;
            ticket.BuyerId = usersRepository.GetUserByUserName(User.Identity.Name).Id;

            ticketsRepository.RemoveTicket(ticketId);
            ticketsRepository.AddTicket(ticket);
            orderRepository.AddOrder(new Order
            {
                Id = ++OrdersRepository.IdCounter,
                TicketId = ticketId,
                Status = OrderStatuses.WaitingForConfirmation,
                UserId = usersRepository.GetUserByUserName(User.Identity.Name).Id,
                TrackingNumber = "No tracking number yet"
            });

            return View("InstructionOrderAdded");
        }

        public IActionResult CancelOrder(int orderId)
        {
            var order = orderRepository.GetOrder(orderId);
            var ticket = ticketsRepository.GetTicket(order.TicketId);

            ticket.Status = TicketStatuses.Selling;

            ticketsRepository.RemoveTicket(order.TicketId);
            ticketsRepository.AddTicket(ticket);
            orderRepository.DeleteOrder(orderId);

            return View("InstructionOrderCanceled");
        }
    }
}