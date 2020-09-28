using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsReselling.Business.Enums;
using TicketsReselling.Business.Models;

namespace TicketsReselling.Models
{
    public class OrderViewModel
    {
        public MyOrder[] Orders { get; set; }
    }

    public class MyOrder
    {
        public int OrderId { get; set; }
        public OrderStatuses OrderStatus { get; set; }
        public string SellerName { get; set; }
        public int SellerId { get; set; }
        public decimal TicketPrice { get; set; }
        public int EventId { get; set; }
        public string EventName { get; set; }
        public string EventDate { get; set; }
    }
}
