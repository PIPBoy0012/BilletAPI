using System;
using System.Collections.Generic;

#nullable disable

namespace BilletAPI.Models
{
    public partial class User
    {
        public User()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
