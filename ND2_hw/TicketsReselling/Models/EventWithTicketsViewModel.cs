using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsReselling.Business.Models;
using TicketsReselling.DAL.Models;

namespace TicketsReselling.Models
{
    public class EventWithTicketsViewModel
    {
        public Event Event { get; set; } 
        public IEnumerable<EventTickets> Tickets { get; set; }

    }

    public class EventTickets
    {
        public int TicketId { get; set; }
        public string SellerName { get; set; }
        public decimal Price { get; set; }
        public string SellerNotes { get; set; }
    }
}
