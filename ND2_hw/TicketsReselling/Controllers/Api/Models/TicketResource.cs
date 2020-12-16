using System;
using TicketsReselling.DAL.Enums;
using TicketsReselling.DAL.Models;

namespace TicketsReselling.Core.Controllers.Api.Models
{
    public class TicketResource
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public TicketStatuses Status { get; set; }
        public string SellerNotes { get; set; }
        public int? ListingId { get; set; }
        public Listing Listing { get; set; }
    }
}