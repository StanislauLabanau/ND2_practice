using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketsReselling.Business.Models
{
    public class Event
    {
        public int Id { get; set; }
        public Category Category { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public Venue Venue { get; set; }
        public string Banner { get; set; }
        public string Description { get; set; }
    }
}
