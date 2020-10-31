using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicketsReselling.Models
{
    public class AddTrackingViewModel
    {
        public int TicketId { get; set; }
        [Required]
        public string TrackingNumber { get; set; }
    }
}
