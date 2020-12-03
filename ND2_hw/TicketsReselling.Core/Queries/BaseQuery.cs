using TicketsReselling.Core.Enums;

namespace TicketsReselling.Core.Queries
{
    public abstract class BaseQuery
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string SearchText { get; set; }
        public SortBy SortBy { get; set; }
        public SortOrder SortOrder { get; set; }
    }
}