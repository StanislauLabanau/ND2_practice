using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsReselling.DAL.Models;

namespace TicketsReselling.Models
{
    public class VenuesViewModel
    {
        public IEnumerable<City> Cities { get; set; }
        public IEnumerable<Venue> Venues { get; set; }
    }
}
