using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsReselling.DAL.Enums;

namespace TicketsReselling.DAL.Models
{
    public class Venue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public City City { get; set; }
        public int CityId { get; set; }
        public string Address { get; set; }
        public VenueStatuses Status { get; set; }
    }
}
