using System.Collections.Generic;
using System.Linq;
using TicketsReselling.Business.Models;

namespace TicketsReselling.Business
{
    public class OrdersRepository
    {
        private List<Order> orders;
        public static int IdCounter { get; set; } = 0;

        public OrdersRepository()
        {
            orders = new List<Order> { };
        }

        public Order[] GetOrders()
        {
            return orders.ToArray();
        }

        public Order GetOrder(int id)
        {
            return orders.FirstOrDefault(t => t.Id == id);
        }

        public Order GetOrderByTicketId(int id)
        {
            return orders.FirstOrDefault(t => t.TicketId == id);
        }

        public void AddOrder(Order order)
        {
            orders.Add(order);
        }

        public void DeleteOrder(int id)
        {
            orders.Remove(GetOrder(id));
        }
    }
}
