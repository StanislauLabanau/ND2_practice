using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketsReselling.DAL.Enums
{
    public enum OrderStatuses
    {
        WaitingForConfirmation,
        Confirmed,
        Rejected,
        Removed,
        Cancelled,
        Completed
    }
}
