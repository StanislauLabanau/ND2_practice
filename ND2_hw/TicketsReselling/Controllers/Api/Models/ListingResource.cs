using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketsReselling.Controllers.Api.Models
{
    public class ListingResource
    {
        public int EventId { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public string Notes { get; set; }
        public string ListingName { get; set; }
    }
}
