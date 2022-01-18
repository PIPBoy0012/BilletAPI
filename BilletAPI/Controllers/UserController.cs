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
    public class UserController : ControllerBase
    {
        private readonly IDataRepository<User> _dataRepository;

        public UserController(IDataRepository<User> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        //GET: api/User
        [HttpGet]
        public IActionResult Get()
        {
            var users = _dataRepository.GetAll();

            return Ok(users);
        }

        //GET: api/User/{id}
        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult Get(long id)
        {
            var user = _dataRepository.Get(id);

            if (user == null)
            {
                return NotFound("The user was not found");
            }

            return Ok(user);
        }

        //POST: api/User
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("The user values were null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _dataRepository.Add(user);

            return Ok("The user has been succesfully created");
        }

        //PUT: api/User/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("The user values were null");
            }

            var userToUpdate = _dataRepository.Get(id);

            if (userToUpdate == null)
            {
                return NotFound("The user was not found");
            }

            _dataRepository.Update(userToUpdate, user);

            return Ok("User successfully updated");
        }

        //DELETE: api/User/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var user = _dataRepository.Get(id);

            if (user == null)
            {
                return NotFound("The user was not found");
            }

            _dataRepository.Delete(user);
            return Ok("User has been deleted");
        }
    }
}