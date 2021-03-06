﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicketsReselling.Core.Queries;
using TicketsReselling.DAL;
using TicketsReselling.DAL.Enums;
using TicketsReselling.DAL.Models;

namespace TicketsReselling.Core.Interfaces
{
    public interface IVenuesService
    {
        public Task<IEnumerable<Venue>> GetVenues();
        public Task<Venue> GetVenueById(int id);
        public Task AddVenue(Venue newCity);
        public Task RemoveVenue(int Id);
        public Task ChangeVenueStatus(Venue venue, VenueStatuses status);
        public Task<IEnumerable<Venue>> GetVenuesByStatuses(params VenueStatuses[] statuses);
        public Task<IEnumerable<Venue>> GetVenuesByStatusesAndQuery(VenueQuery query, params VenueStatuses[] statuses);
    }
}