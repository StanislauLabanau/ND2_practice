using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsReselling.DAL;
using TicketsReselling.DAL.Models;

namespace TicketsReselling.Core
{
    public class OrdersService
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

        public async Task<Order> GetOrerById(int id)
        {
            return await context.Orders.FindAsync(id);
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserId(int userId)
        {
            var userOrders = context.Orders.Where(o => o.UserId == userId);

            return await userOrders.ToListAsync();
        }

        public async Task<Order> GetOrderByTicketId(int ticketId)
        {
            return await context.Orders.FirstOrDefaultAsync(o => o.TicketId == ticketId);
        }

        public async Task ChangeOrderStatus(Order order, int status)
        {
            order.Status = status;
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
            context.Orders.Remove(await GetOrerById(id));
            await context.SaveChangesAsync();
        }


    }
}
