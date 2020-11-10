using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsReselling.DAL.Enums;

namespace TicketsReselling.DAL.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public OrderStatuses Status { get; set; }
        public string UserId { get; set; }
        public string TrackingNumber { get; set; }
    }
}
