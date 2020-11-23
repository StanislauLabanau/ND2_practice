
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsReselling.DAL;
using TicketsReselling.DAL.Models;
using TicketsReselling.DAL.Enums;
using TicketsReselling.Core.Interfaces;

namespace TicketsReselling.Core
{
    public class OrdersService : IOrdersService
    {
        private readonly TicketsResellingContext context;

        public OrdersService(TicketsResellingContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            var orders = context.Orders;

            return await orders.ToListAsync();
        }

        public async Task<Order> GetOrderById(int id)
        {
            return await context.Orders.FindAsync(id);
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserId(string userId)
        {
            var userOrders = context.Orders.Where(o => o.UserId.Equals(userId));

            return await userOrders.ToListAsync();
        }

        public async Task<Order> GetOrderByTicketIdAndStatuses(int ticketId, params OrderStatuses[] statuses)
        {
            return await context.Orders.FirstOrDefaultAsync(o => o.TicketId == ticketId && statuses.Contains(o.Status));
        }

        public async Task ChangeOrderStatus(Order order, OrderStatuses status)
        {
            order.Status = status;
            context.Orders.Update(order);
            await context.SaveChangesAsync();
        }

        public async Task ChangeOrderTracking(Order order, string trackingNumber)
        {
            order.TrackingNumber = trackingNumber;
            context.Orders.Update(order);
            await context.SaveChangesAsync();
        }

        public async Task AddOrder(Order newOrder)
        {
            context.Orders.Add(newOrder);
            await context.SaveChangesAsync();
        }

        public async Task RemoveOrder(int id)
        {
            context.Orders.Remove(await GetOrderById(id));
            await context.SaveChangesAsync();
        }
    }
}
