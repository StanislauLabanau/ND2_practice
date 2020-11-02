using System;
using System.Collections.Generic;
using System.Text;

namespace TicketsReselling.DAL
{
    interface IEntity<T>
    {
        public T Id { get; set; }
    }
}
