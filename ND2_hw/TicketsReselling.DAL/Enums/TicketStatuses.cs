using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketsReselling.DAL.Enums
{
    public enum TicketStatuses
    {
        WaitingForConfirmation,
        WaitingForReceivingConfirmation,
        Selling,
        Sold,
        Removed
    }
}
