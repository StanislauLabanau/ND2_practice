using System;

namespace TicketsReselling.Core.Queries
{
    public class EventQuery: BaseQuery
    {
        public int[] Categories { get; set; }
        public int[] Cities { get; set; }
        public int[] Venues { get; set; }
        public bool WithUserTicketsOnly { get; set; }
        public string CurrentUserId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}