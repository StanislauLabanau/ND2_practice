﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsReselling.DAL;
using TicketsReselling.DAL.Models;

namespace TicketsReselling.Core
{
    public class EventsService
    {
        private readonly TicketsResellingContext context;

        public EventsService(TicketsResellingContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await context.Categories.FindAsync(id);
        }

        public async Task<IEnumerable<Event>> GetEvents()
        {
            return await context.Events.ToListAsync();
        }

        public async Task<Event> GetEventById(int id)
        {
            return await context.Events.FindAsync(id);
        }

        public async Task AddEvent(Event newEvent)
        {
            context.Events.Add(newEvent);
            await context.SaveChangesAsync();
        }

        public async void RemoveEvent(int id)
        {
            context.Events.Remove(await GetEventById(id));
            await context.SaveChangesAsync();
        }
    }
}
