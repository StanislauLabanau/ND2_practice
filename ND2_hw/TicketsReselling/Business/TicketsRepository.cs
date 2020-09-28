using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsReselling.Business.Enums;
using TicketsReselling.Business.Models;

namespace TicketsReselling.Business
{
    public class TicketsRepository
    {
        private List<Ticket> tickets;
        public static int IdCounter { get; set; } = 7;

        public TicketsRepository()
        {
            tickets = new List<Ticket>
            {
                new Ticket { Id = 1,  EventId = 1, Price = 110, SellerId = 1, Status = TicketStatuses.Selling, SellerNotes="Notes" },
                new Ticket { Id = 2,  EventId = 1, Price = 100, SellerId = 2, Status = TicketStatuses.Selling, SellerNotes="Notes" },
                new Ticket { Id = 3,  EventId = 1, Price = 110, SellerId = 3, Status = TicketStatuses.Selling, SellerNotes="Notes" },
                new Ticket { Id = 4,  EventId = 2, Price = 100, SellerId = 1, Status = TicketStatuses.Selling, SellerNotes="Notes" },
                new Ticket { Id = 5,  EventId = 4, Price = 90, SellerId = 4, Status = TicketStatuses.Selling, SellerNotes="Notes" },
                new Ticket { Id = 6,  EventId = 5, Price = 90, SellerId = 4, Status = TicketStatuses.Selling, SellerNotes="Notes" },
                new Ticket { Id = 7,  EventId = 6, Price = 90, SellerId = 4, Status = TicketStatuses.Selling, SellerNotes="Notes" },
            };
        }

        public Ticket[] GetTickets()
        {
            return tickets.ToArray();
        }

        public Ticket GetTicket(int id)
        {
            return tickets.FirstOrDefault(t => t.Id == id);
        }

        public void AddTicket(Ticket ticket)
        {
            tickets.Add(ticket);
        }

        public void RemoveTicket(int Id)
        {
            tickets.Remove(GetTicket(Id));
        }
    }
}
