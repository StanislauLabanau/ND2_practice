using System;
using System.Linq;
using System.Linq.Expressions;
using TicketsReselling.Core.Enums;

namespace TicketsReselling.Core.Queries
{
    public abstract class BaseSortingProvider<T> : ISortingProvider<T>
    {
        public IOrderedQueryable<T> ApplySorting(IQueryable<T> queryable, BaseQuery query)
        {
            var sortExpression = GetSortExpression(query);

            return query.SortOrder switch
            {
                SortOrder.Desc => queryable.OrderByDescending(sortExpression),
                SortOrder.Asc => queryable.OrderBy(sortExpression)
            };
        }

        protected abstract Expression<Func<T, object>> GetSortExpression(BaseQuery query);
    }
}