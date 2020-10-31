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
        public TicketView[] Tickets { get; set; }
    }

    public class TicketView
    {
        public int TicketId{ get; set; }
        public int TicketStatus{ get; set; }
        public string BuyerName  { get; set; }
        public int BuyerId  { get; set; }
        public decimal TicketPrice { get; set; }
        public string EventName { get; set; }
        public int EventId { get; set; }
        public int OrderId { get; set; }
        public string OrderTrackingNumber { get; set; }
        public DateTime EventDate { get; set; }
    }
}
