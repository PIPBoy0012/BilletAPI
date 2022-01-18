using System;
using System.Collections.Generic;

#nullable disable

namespace BilletAPI.Models
{
    public partial class Event
    {
        public Event()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int EventId { get; set; }
        public string EventName { get; set; }
        public string EventStrtName { get; set; }
        public int EventZipCode { get; set; }
        public DateTime EventDate { get; set; }
        public TimeSpan EventTime { get; set; }

        public virtual ZipCode EventZipCodeNavigation { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
