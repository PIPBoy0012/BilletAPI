using System;
using System.Collections.Generic;

#nullable disable

namespace BilletAPI.Models
{
    public partial class Ticket
    {
        public int TicketId { get; set; }
        public int EventId { get; set; }
        public bool IsUsed { get; set; }
        public int UserId { get; set; }

        public virtual Event Event { get; set; }
        public virtual User User { get; set; }
    }
}
