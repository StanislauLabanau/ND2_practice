using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketsReselling.DAL.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public int Status { get; set; }
        public string UserId { get; set; }
        public string TrackingNumber { get; set; }
    }
}
