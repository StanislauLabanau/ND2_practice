﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicketsReselling.DAL;
using TicketsReselling.DAL.Enums;
using TicketsReselling.DAL.Models;

namespace TicketsReselling.Core.Interfaces
{
    public interface ITicketsService
    {
        public Task<IEnumerable<Ticket>> GetTickets();
        public Task<Ticket> GetTicketById(int id);
        public Task AddTicket(Ticket newCity);
        public Task RemoveTicket(int Id);
        public Task<IEnumerable<Ticket>> GetTicketsByUserId(string userId);
        public IEnumerable<Ticket> GetTicketsByEventIdAndStatus(int eventId, TicketStatuses status);
        public Task<IEnumerable<Ticket>> GetUserTicketsByEventId(int eventId, string userId);
        public Task ChangeTicketStatus(Ticket ticket, TicketStatuses status);
        public Task AddListingWithTickets(Ticket newTicket, int amount, string listingName);
    }
}