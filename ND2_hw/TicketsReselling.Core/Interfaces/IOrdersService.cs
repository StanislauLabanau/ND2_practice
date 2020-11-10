using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicketsReselling.DAL;
using TicketsReselling.DAL.Enums;
using TicketsReselling.DAL.Models;

namespace TicketsReselling.Core.Interfaces
{
    public interface IOrdersService
    {
        public Task<IEnumerable<Order>> GetOrders();
        public Task<Order> GetOrderById(int id);
        public Task AddOrder(Order newCity);
        public Task RemoveOrder(int Id);
        public Task<IEnumerable<Order>> GetOrdersByUserId(string userId);
        public Task<Order> GetOrderByTicketIdAndStatuses(int ticketId, params OrderStatuses[] statuses);
        public Task ChangeOrderStatus(Order order, OrderStatuses status);
        public Task ChangeOrderTracking(Order order, string trackingNumber);
    }
}