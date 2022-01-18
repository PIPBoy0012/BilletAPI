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
    public class ZipCodeController : ControllerBase
    {
        private readonly IDataRepository<ZipCode> _dataRepository;

        public ZipCodeController(IDataRepository<ZipCode> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        //GET: api/ZipCode
        [HttpGet]
        public IActionResult Get()
        {
            var zipCodes = _dataRepository.GetAll();

            return Ok(zipCodes);
        }

        //GET: api/ZipCode/{id}
        [HttpGet("{id}", Name = "GetZipCode")]
        public IActionResult Get(long id)
        {
            var zipCode = _dataRepository.Get(id);

            if (zipCode == null)
            {
                return NotFound("The ZipCode was not found");
            }

            return Ok(zipCode);
        }

        //POST: api/ZipCode
        [HttpPost]
        public IActionResult Post([FromBody] ZipCode zipCode)
        {
            if (zipCode == null)
            {
                return BadRequest("The ZipCode values were null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _dataRepository.Add(zipCode);

            return Ok("The ZipCode has been succesfully created");
        }

        //PUT: api/ZipCode/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ZipCode zipCode)
        {
            if (zipCode == null)
            {
                return BadRequest("The ZipCode values were null");
            }

            var zipCodeToUpdate = _dataRepository.Get(id);

            if (zipCodeToUpdate == null)
            {
                return NotFound("The ZipCode was not found");
            }

            _dataRepository.Update(zipCodeToUpdate, zipCode);

            return Ok("ZipCode successfully updated");
        }

        //DELETE: api/ZipCode/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var zipCode = _dataRepository.Get(id);

            if (zipCode == null)
            {
                return NotFound("The ZipCode was not found");
            }

            _dataRepository.Delete(zipCode);
            return Ok("ZipCode has been deleted");
        }
    }
}