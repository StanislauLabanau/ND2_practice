using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsReselling.Business.Models;

namespace TicketsReselling.Business
{
    public class OrdersRepository
    {
        private readonly List<Order> orders;
        private readonly List<OrderStatus> statuses;

        public OrdersRepository()
        {
            statuses = new List<OrderStatus>
            {
                new OrderStatus{Id = 1, Name = "Waiting for confirmation"},
                new OrderStatus{Id = 2, Name = "Confirmed"},
                new OrderStatus{Id = 3, Name = "Rejected"},
            };

            orders = new List<Order>
            {
                new Order { Id = 1, TicketId = 1, UserId = 4, Status = statuses[0], TrackingNumber = "0"},
                new Order { Id = 2, TicketId = 2, UserId = 4, Status = statuses[0], TrackingNumber = "0"},
                new Order { Id = 3, TicketId = 3, UserId = 4, Status = statuses[0], TrackingNumber = "0"},
                new Order { Id = 4, TicketId = 4, UserId = 4, Status = statuses[0], TrackingNumber = "0"},
            };
        }

        public Order[] GetOrders()
        {
            return orders.ToArray();
        }

        public OrderStatus[] GetStatuses()
        {
            return statuses.ToArray();
        }
    }
}
