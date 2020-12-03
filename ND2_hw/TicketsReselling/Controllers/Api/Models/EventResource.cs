using System;

namespace TicketsReselling.Core.Controllers.Api.Models
{
    public class EventResource
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string VenueName { get; set; }
        public string CityName { get; set; }
        public string Description { get; set; }
        public string Banner { get; set; }
        public DateTime Date { get; set; }
    }
}