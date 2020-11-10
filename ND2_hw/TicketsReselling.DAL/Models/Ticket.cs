using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsReselling.DAL.Enums;

namespace TicketsReselling.DAL.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public decimal Price { get; set; }
        public string SellerId { get; set; }
        public User Seller { get; set; }
        public TicketStatuses Status { get; set; }
        public string SellerNotes { get; set; }
    }
}
