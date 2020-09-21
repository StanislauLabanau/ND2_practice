using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsReselling.Business.Models;

namespace TicketsReselling.Models
{
    public class MyTicketsViewModel
    {
        public TicketStatus[] Statuses { get; set; }
        public MyTicket[] Tickets { get; set; }
    }

    public class MyTicket
    {
        public int TicketId{ get; set; }
        public TicketStatus TicketStatus{ get; set; }
        public string BuyerName  { get; set; }
        public decimal TicketPrice { get; set; }
        public string EventName { get; set; }
        public string EventDate { get; set; }
    }
}
