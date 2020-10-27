using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsReselling.Business.Models;
using TicketsReselling.DAL.Models;

namespace TicketsReselling.Models
{
    public class EventsViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Event> Events { get; set; }
    }
}
