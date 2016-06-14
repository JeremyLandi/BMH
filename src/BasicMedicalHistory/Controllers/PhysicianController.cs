using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using System.Web.Http;
using BasicMedicalHistory.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BasicMedicalHistory.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [EnableCors("AllowDevelopmentEnvironment")]
    public class PhysicianController : ApiController
    {

        private BmhContext _context;

        public PhysicianController(BmhContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult GetAllergy([FromQuery]int? id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IQueryable<Physician> physician = (from a in _context.Physician
                                               where a.CustomerId == id
                                               select new Physician
                                               {
                                                   PhysicianId = a.PhysicianId,
                                                   PhysicianName = a.PhysicianName,
                                                   Title = a.Title,
                                                   BusinessName = a.BusinessName,
                                                   Phone = a.Phone,
                                                   Address = a.Address,
                                                   City = a.City,
                                                   State = a.State,
                                                   CustomerId = a.CustomerId
                                               });

            if (physician == null)
            {
                return NotFound();
            }

            return Ok(physician);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Physician physician)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingPhysician = (from a in _context.Physician
                                     where a.PhysicianName == physician.PhysicianName
                                     && a.CustomerId == physician.CustomerId
                                     select a);

            //if physician exists, it won't create another
            if (existingPhysician.Count<Physician>() > 0)
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }

            _context.Physician.Add(physician);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PhysicianExists(physician.PhysicianId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            //return CreatedAtRoute("GetPhysician", new { id = physician.PhysicianId }, physician);
            return Ok(physician);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Physician physician)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != physician.PhysicianId)
            {
                return BadRequest();
            }

            _context.Entry(physician).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhysicianExists(physician.PhysicianId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return new StatusCodeResult(StatusCodes.Status204NoContent);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Physician physician = _context.Physician.Single(c => c.PhysicianId == id);
            if (physician == null)
            {
                return NotFound();
            }

            _context.Physician.Remove(physician);
            _context.SaveChanges();

            return Ok(physician);
        }

        private bool PhysicianExists(int id)
        {
            return _context.Physician.Count(c => c.PhysicianId == id) > 0;
        }
    }
}