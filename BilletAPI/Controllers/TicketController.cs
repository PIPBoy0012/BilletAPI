using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using BilletAPI.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using BilletAPI.Models;

namespace BilletAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly IDataRepository<Ticket> _dataRepository;

        public TicketController(IDataRepository<Ticket> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        //GET: api/Ticket
        [HttpGet]
        public IActionResult Get()
        {
            var tickets = _dataRepository.GetAll();

            return Ok(tickets);
        }

        //GET: api/Ticket/{id}
        [HttpGet("{id}", Name = "GetTicket")]
        public IActionResult Get(long id)
        {
            var ticket = _dataRepository.Get(id);

            if (ticket == null)
            {
                return NotFound("The Ticket was not found");
            }

            return Ok(ticket);
        }

        //POST: api/Ticket
        [HttpPost]
        public IActionResult Post([FromBody] Ticket ticket)
        {
            if (ticket == null)
            {
                return BadRequest("The Ticket values were null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _dataRepository.Add(ticket);

            return Ok("The Ticket has been succesfully created");
        }

        //PUT: api/Ticket/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Ticket ticket)
        {
            if (ticket == null)
            {
                return BadRequest("The Ticket values were null");
            }

            var ticketToUpdate = _dataRepository.Get(id);

            if (ticketToUpdate == null)
            {
                return NotFound("The Ticket was not found");
            }

            _dataRepository.Update(ticketToUpdate, ticket);

            return Ok("Ticket successfully updated");
        }

        //DELETE: api/Ticket/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var ticket = _dataRepository.Get(id);

            if (ticket == null)
            {
                return NotFound("The Ticket was not found");
            }

            _dataRepository.Delete(ticket);
            return Ok("Ticket has been deleted");
        }
    }
}