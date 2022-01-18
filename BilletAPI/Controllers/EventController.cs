using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BilletAPI.Models.Repository;
using BilletAPI.Models;

namespace BilletAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IDataRepository<Event> _dataRepository;

        public EventController(IDataRepository<Event> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        //GET: api/event
        [HttpGet]
        public IActionResult Get()
        {
            var events = _dataRepository.GetAll();

            return Ok(events);
        }

        //GET: api/event/{id}
        [HttpGet("{id}", Name = "GetEvent")]
        public IActionResult Get(long id)
        {
            var @event = _dataRepository.Get(id);

            if (@event == null)
            {
                return NotFound("The Event was not found");
            }

            return Ok(@event);
        }

        //POST: api/event
        [HttpPost]
        public IActionResult Post([FromBody] Event @event)
        {
            if (@event == null)
            {
                return BadRequest("The event values were null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _dataRepository.Add(@event);

            return Ok("The event has been succesfully created");
        }

        //PUT: api/event/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Event @event)
        {
            if (@event == null)
            {
                return BadRequest("The event values were null");
            }

            var eventToUpdate = _dataRepository.Get(id);

            if (eventToUpdate == null)
            {
                return NotFound("The event was not found");
            }

            _dataRepository.Update(eventToUpdate, @event);

            return Ok("event successfully updated");
        }

        //DELETE: api/event/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var @event = _dataRepository.Get(id);

            if (@event == null)
            {
                return NotFound("The event was not found");
            }

            _dataRepository.Delete(@event);
            return Ok("event has been deleted");
        }
    }
}