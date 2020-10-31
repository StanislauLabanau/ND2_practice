﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketsReselling.DAL.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public decimal Price { get; set; }
        public int SellerId { get; set; }
        public int Status { get; set; }
        public string SellerNotes { get; set; }
    }
}