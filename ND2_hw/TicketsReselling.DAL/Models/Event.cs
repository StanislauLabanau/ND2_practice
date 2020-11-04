using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsReselling.DAL.Enums;
using TicketsReselling.DAL.Models;

namespace TicketsReselling.DAL.Models
{
    public class Event : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public Venue Venue { get; set; }
        public int VenueId { get; set; }
        public DateTime Date { get; set; }
        public string Banner { get; set; }
        public EventStatuses Status { get; set; }
        public string Description { get; set; }
    }
}
