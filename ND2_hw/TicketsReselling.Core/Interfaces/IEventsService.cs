using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicketsReselling.DAL;
using TicketsReselling.DAL.Enums;
using TicketsReselling.DAL.Models;

namespace TicketsReselling.Core.Interfaces
{
    public interface IEventsService
    {
        public Task<IEnumerable<Category>> GetCategories();
        public Task<Category> GetCategoryById(int id);
        public Task<Event> GetEventById(int id);
        public Task AddEvent(Event newEvent);
        public Task RemoveEvent(int id);
        public Task<IEnumerable<Event>> GetEventsByStatus(EventStatuses status);
        public Task ChangeEventStatus(Event eventItem, EventStatuses status);
    }
}