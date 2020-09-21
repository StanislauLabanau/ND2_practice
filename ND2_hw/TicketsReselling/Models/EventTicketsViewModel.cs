using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsReselling.Business.Models;

namespace TicketsReselling.Models
{
    public class EventTicketsViewModel
    {
        public Event Event { get; set; } 
        public EventTickets[] Tickets { get; set; }

        public class EventTickets
        {
            public int Id { get; set; }
            public string SellerName { get; set; }
            public decimal Price { get; set; }
            public string SellerNotes { get; set; }
        }
    }
}
