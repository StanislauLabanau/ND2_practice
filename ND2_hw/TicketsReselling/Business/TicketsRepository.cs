using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsReselling.Business.Models;

namespace TicketsReselling.Business
{
    public class TicketsRepository
    {
        private readonly List<Ticket> tickets;
        private readonly List<TicketStatus> statuses;

        public TicketsRepository()
        {
            statuses = new List<TicketStatus>
            {
                new TicketStatus{Id = 1, Name = "Waiting for confirmation"},
                new TicketStatus{Id = 2, Name = "Selling"},
                new TicketStatus{Id = 3, Name = "Sold"},
            };

            tickets = new List<Ticket>
            {
                new Ticket { Id = 1,  EventId = 1, Price = 110, SellerId = 1, BuyerId = 4, Status = statuses[0], SellerNotes="Notes" },
                new Ticket { Id = 2,  EventId = 1, Price = 100, SellerId = 2, BuyerId = 4, Status = statuses[0], SellerNotes="Notes" },
                new Ticket { Id = 3,  EventId = 1, Price = 110, SellerId = 3, BuyerId = 4, Status = statuses[0], SellerNotes="Notes" },
                new Ticket { Id = 4,  EventId = 2, Price = 100, SellerId = 1, BuyerId = 4, Status = statuses[0], SellerNotes="Notes" },
                new Ticket { Id = 5,  EventId = 4, Price = 90, SellerId = 4, Status = statuses[1], SellerNotes="Notes" },
                new Ticket { Id = 6,  EventId = 5, Price = 90, SellerId = 4, Status = statuses[1], SellerNotes="Notes" },
                new Ticket { Id = 7,  EventId = 6, Price = 90, SellerId = 4, Status = statuses[1], SellerNotes="Notes" },
            };
        }

        public Ticket[] GetTickets()
        {
            return tickets.ToArray();
        }

        public Ticket GetTicketById(int id)
        {
            return tickets.FirstOrDefault(t => t.Id == id);
        }

        public TicketStatus[] GetStatuses()
        {
            return statuses.ToArray();
        }
    }
}
