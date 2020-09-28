using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsReselling.Business.Enums;
using TicketsReselling.Business.Models;

namespace TicketsReselling.Models
{
    public class TicketsViewModel
    {
        public MyTicket[] Tickets { get; set; }
    }

    public class MyTicket
    {
        public int TicketId{ get; set; }
        public TicketStatuses TicketStatus{ get; set; }
        public string BuyerName  { get; set; }
        public int BuyerId  { get; set; }
        public decimal TicketPrice { get; set; }
        public string EventName { get; set; }
        public int EventId { get; set; }
        public string EventDate { get; set; }
    }
}
