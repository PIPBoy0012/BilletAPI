using System;
using System.Collections.Generic;
using System.Linq;
using BilletAPI.Models.Repository;
namespace BilletAPI.Models.DataManager
{
    public class EventManager : IDataRepository<Event>
    {
        readonly BilletSystemAPIContext _skpDbContext;

        public EventManager(BilletSystemAPIContext sKPDbContext)
        {
            _skpDbContext = sKPDbContext;
        }

        public IEnumerable<Event> GetAll()
        {
            return _skpDbContext.Events.ToList();
        }

        public Event Get(long id)
        {
            return _skpDbContext.Events.FirstOrDefault(e => e.EventId == id);
        }

        public void Add(Event @event)
        {
            _skpDbContext.Add(@event);
            _skpDbContext.SaveChanges();
        }

        public void Update(Event eventToChange, Event @event)
        {
            eventToChange.EventDate = @event.EventDate;
            eventToChange.EventName = @event.EventName;
            eventToChange.EventStrtName = @event.EventStrtName;
            eventToChange.EventTime = @event.EventTime;
            eventToChange.EventZipCode = @event.EventZipCode;

            _skpDbContext.SaveChanges();
        }

        public void Delete(Event @event)
        {
            _skpDbContext.Remove(@event);
            _skpDbContext.SaveChanges();
        }

    }
}
