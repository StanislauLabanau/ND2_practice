using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsReselling.Core.Enums;
using TicketsReselling.DAL.Models;

namespace TicketsReselling.Models
{
    public class EventsViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<City> Cities { get; set; }
        public IEnumerable<Venue> Venues { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public SortBy SortBy { get; set; }
        public SortOrder SortOrder { get; set; }
        public int PageSize { get; set; }
        public string SearchText { get; set; }
    }
}
