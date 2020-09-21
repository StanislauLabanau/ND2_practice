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
    public class MyOrdersController : Controller
    {
        private readonly TicketsRepository ticketsRepository;
        private readonly UsersRepository usersRepository;
        private readonly EventsRepository eventsRepository;
        private readonly OrdersRepository orderRepository;

        public MyOrdersController(TicketsRepository ticketsRepository, UsersRepository usersRepository, EventsRepository eventsRepository, OrdersRepository orderRepository)
        {
            this.ticketsRepository = ticketsRepository;
            this.usersRepository = usersRepository;
            this.eventsRepository = eventsRepository;
            this.orderRepository = orderRepository;
        }

        [Authorize(Roles = "User")]
        public IActionResult Index()
        {
            var currentUser = usersRepository.GetUserByUserName(User.Identity.Name);
            var userOrders = orderRepository.GetOrders().Where(o => o.UserId == currentUser.Id);

            var myOrders = new List<MyOrder> { };

            foreach (var order in userOrders)
            {
                var orderTicket = ticketsRepository.GetTicketById(order.TicketId);
                var ticketEvent = eventsRepository.GetEventById(orderTicket.EventId);

                myOrders.Add(
                    new MyOrder
                    {
                        OrderId = order.Id,
                        TicketPrice = orderTicket.Price,
                        OrderStatus = order.Status,
                        SellerName = $"{usersRepository.GetUserById(orderTicket.SellerId)?.FirstName} {usersRepository.GetUserById(orderTicket.SellerId)?.SecondName}",
                        EventName = ticketEvent.Name,
                        EventDate = ticketEvent.Date,
                    }
                );
            };

            var model = new MyOrderViewModel
            {
                Statuses = orderRepository.GetStatuses(),
                Orders = myOrders.ToArray()
            };

            return View(model);
        }
    }
}