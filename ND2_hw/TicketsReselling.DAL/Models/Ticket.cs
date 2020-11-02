﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketsReselling.DAL.Models
{
    public class Ticket : IEntity<int>
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public decimal Price { get; set; }
        public string SellerId { get; set; }
        public User Seller { get; set; }
        public int Status { get; set; }
        public string SellerNotes { get; set; }
    }
}
