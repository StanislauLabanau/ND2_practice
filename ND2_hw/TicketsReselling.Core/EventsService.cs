using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsReselling.DAL;
using TicketsReselling.DAL.Models;
using TicketsReselling.DAL.Enums;
using TicketsReselling.Core.Interfaces;
using TicketsReselling.Core.Queries;

namespace TicketsReselling.Core
{
    public class EventsService : IEventsService
    {
        private readonly TicketsResellingContext context;
        private readonly ISortingProvider<Event> sortingProvider;

        public EventsService(TicketsResellingContext context, ISortingProvider<Event> sortingProvider)
        {
            this.context = context;
            this.sortingProvider = sortingProvider;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await context.Categories.FindAsync(id);
        }

        public async Task<IEnumerable<Event>> GetEventsByStatus(EventStatuses status)
        {
            return await context.Events.Where(e => e.Status == status).ToListAsync();
        }

        public async Task<IEnumerable<Event>> GetSuggestedEvents(EventStatuses status, EventQuery query, int suggestedEventsNumber)
        {

            var queryable = context.Events.Include(q => q.Venue).ThenInclude(v => v.City).Where(e => e.Status == status);
            var suggestedEvents = queryable.Where(e => e.Name.Contains(query.SearchText));

            if (query.SearchText != null)
            {
                suggestedEvents = queryable.Where(e => e.Name.Contains(query.SearchText));
            }

            if (suggestedEvents.Count() < suggestedEventsNumber)
            {
                suggestedEvents = suggestedEvents.Concat(queryable.Where(e => e.Venue.City.Name.Contains(query.SearchText)));
            }

            if (suggestedEvents.Count() < suggestedEventsNumber)
            {
                suggestedEvents = suggestedEvents.Concat(queryable.Where(e => e.Venue.Name.Contains(query.SearchText)));
            }

            if (suggestedEvents.Count() > 10)
            {
                suggestedEvents = suggestedEvents.Take(suggestedEventsNumber);
            }

            return await suggestedEvents.ToListAsync();
        }

        public async Task<PagedResult<Event>> GetEventsByStatusAndQuery(EventStatuses status, EventQuery query)
        {
            var queryable = context.Events.Include(q => q.Venue).ThenInclude(v => v.City).Where(e => e.Status == status);

            if (query.SearchText != null)
            {
                queryable = queryable.Where(e => e.Name.Contains(query.SearchText));
            }

            if (query.Categories != null)
            {
                queryable = queryable.Where(e => query.Categories.Contains(e.CategoryId));
            }

            if (query.Cities != null)
            {
                queryable = queryable.Where(e => query.Cities.Contains(e.Venue.CityId));
            }

            if (query.Venues != null)
            {
                queryable = queryable.Where(e => query.Venues.Contains(e.VenueId));
            }

            if (query.FromDate != null)
            {
                queryable = queryable.Where(e => e.Date >= query.FromDate);
            }

            if (query.ToDate != null)
            {
                queryable = queryable.Where(e => e.Date <= query.ToDate);
            }

            if (query.WithUserTicketsOnly)
            {
                var userTickets = context.Tickets.Where(t => t.SellerId.Equals(query.CurrentUserId) && t.Status != TicketStatuses.Removed).ToArray();
                var userTicketsEventsIds = userTickets.Select(t => t.EventId).Distinct();

                queryable = queryable.Where(e => userTicketsEventsIds.Contains(e.Id));
            }

            var count = await queryable.CountAsync();

            queryable = sortingProvider.ApplySorting(queryable, query);
            queryable = queryable.ApplyPagination(query);

            var eventItems = await queryable.ToListAsync();

            return new PagedResult<Event> { TotalCount = count, Items = eventItems };
        }

        public async Task<Event> GetEventById(int id)
        {
            return await context.Events.FindAsync(id);
        }

        public async Task ChangeEventStatus(Event eventItem, EventStatuses status)
        {
            eventItem.Status = status;
            context.Events.Update(eventItem);
            await context.SaveChangesAsync();
        }

        public async Task AddEvent(Event newEvent)
        {
            context.Events.Add(newEvent);
            await context.SaveChangesAsync();
        }

        public async Task RemoveEvent(int id)
        {
            context.Events.Remove(await GetEventById(id));
            await context.SaveChangesAsync();
        }
    }
}
