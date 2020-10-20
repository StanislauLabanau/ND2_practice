using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsReselling.Business.Models;

namespace TicketsReselling.Models
{
    public class EventsViewModel
    {
        public Category[] Categories { get; set; }
        public Event[] Events { get; set; }
    }
}
