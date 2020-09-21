using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketsReselling.Business.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public decimal Price { get; set; }
        public int SellerId { get; set; }
        public int BuyerId { get; set; }
        public TicketStatus Status { get; set; }
        public string SellerNotes { get; set; }
    }
}
