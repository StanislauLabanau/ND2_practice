using System;
using System.Linq.Expressions;
using TicketsReselling.Core.Enums;
using TicketsReselling.DAL.Models;

namespace TicketsReselling.Core.Queries
{
    public class EventSortingProvider: BaseSortingProvider<Event>
    {
        protected override Expression<Func<Event, object>> GetSortExpression(BaseQuery query)
        {
            return query.SortBy switch
            {
                SortBy.Date => p => p.Date,
                SortBy.Name => p => p.Name
            };
        }
    }
}