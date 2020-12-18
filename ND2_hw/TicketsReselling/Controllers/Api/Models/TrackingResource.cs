using System;
using TicketsReselling.DAL.Enums;
using TicketsReselling.DAL.Models;

namespace TicketsReselling.Core.Controllers.Api.Models
{
    public class TrackingResource
    {
        public int ticketId { get; set; }
        public string TrackingNumber { get; set; }
    }
}