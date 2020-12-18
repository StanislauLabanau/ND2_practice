using System;
using System.Collections.Generic;
using System.Text;

namespace TicketsReselling.DAL.Models
{
    public class Listing
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }

    }
}
