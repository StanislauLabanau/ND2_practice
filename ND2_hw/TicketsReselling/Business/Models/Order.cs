using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketsReselling.Business.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public OrderStatus Status { get; set; }
        public int UserId { get; set; }
        public string TrackingNumber { get; set; }
    }
}
