using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsReselling.DAL.Enums;

namespace TicketsReselling.DAL.Models
{
    public class City : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CityStatuses Status { get; set; }
    }
}
