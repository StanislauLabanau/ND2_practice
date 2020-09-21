using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsReselling.Business.Models;

namespace TicketsReselling.Models
{
    public class MyOrderViewModel
    {
        public OrderStatus[] Statuses { get; set; }
        public MyOrder[] Orders { get; set; }
    }

    public class MyOrder
    {
        public int OrderId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string SellerName { get; set; }
        public decimal TicketPrice { get; set; }
        public string EventName { get; set; }
        public string EventDate { get; set; }
    }
}
