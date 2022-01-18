using System;
using System.Collections.Generic;
using System.Linq;
using BilletAPI.Models.Repository;
namespace BilletAPI.Models.DataManager
{
    public class TicketManager : IDataRepository<Ticket>
    {
        readonly BilletSystemAPIContext _sKPDbContext;

        public TicketManager(BilletSystemAPIContext sKPDbContext)
        {
            _sKPDbContext = sKPDbContext;
        }

        public IEnumerable<Ticket> GetAll()
        {
            return _sKPDbContext.Tickets.ToList();
        }

        public Ticket Get(long id)
        {
            return _sKPDbContext.Tickets.FirstOrDefault(e => e.TicketId == id);
        }

        public void Add(Ticket ticket)
        {
            _sKPDbContext.Add(ticket);
        }

        public void Update(Ticket ticketToUpdate, Ticket ticket)
        {
            ticketToUpdate.EventId = ticket.EventId;
            ticketToUpdate.IsUsed = ticket.IsUsed;
            ticketToUpdate.UserId = ticket.UserId;

            _sKPDbContext.SaveChanges();
        }

        public void Delete(Ticket ticket)
        {
            _sKPDbContext.Remove(ticket);
            _sKPDbContext.SaveChanges();
        }
    }
}
