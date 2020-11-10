using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsReselling.DAL.Enums;

namespace TicketsReselling.Models
{
    public class OrderViewModel
    {
        public OrderView[] Orders { get; set; }
    }

    public class OrderView
    {
        public int OrderId { get; set; }
        public OrderStatuses OrderStatus { get; set; }
        public string SellerName { get; set; }
        public string SellerId { get; set; }
        public decimal TicketPrice { get; set; }
        public int EventId { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
    }
}
