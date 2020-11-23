using System.Linq;

namespace TicketsReselling.Core.Queries
{
    public interface ISortingProvider<T>
    {
        IOrderedQueryable<T> ApplySorting(IQueryable<T> queryable, BaseQuery query);
    }
}