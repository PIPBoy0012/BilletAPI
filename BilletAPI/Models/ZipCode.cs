using System;
using System.Collections.Generic;

#nullable disable

namespace BilletAPI.Models
{
    public partial class ZipCode
    {
        public ZipCode()
        {
            Events = new HashSet<Event>();
        }

        public int PostalCode { get; set; }
        public string City { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}
