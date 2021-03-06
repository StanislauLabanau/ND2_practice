﻿using System.Collections.Generic;

namespace TicketsReselling.Core.Queries
{
    public class PagedResult<T>
    {
        public int TotalCount { get; set; }
        public ICollection<T> Items { get; set; }
    }
}