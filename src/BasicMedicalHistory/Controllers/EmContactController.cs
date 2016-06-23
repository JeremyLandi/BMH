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
    public class EmContactController : ApiController
    {
        private BmhContext _context;

        public EmContactController(BmhContext context)
        {
            _context = context;
        }

        // get emContact by
        [HttpGet]
        public IActionResult GetEmContact([FromQuery]int? id, [FromQuery] string token, [FromQuery]string custUserName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //checks if they are logged in
            if (token == null)
            {
                IQueryable<EmContact> emContact = (from a in _context.EmContact
                                                   where a.CustUserName == custUserName
                                                   && a.ShowOnPublicView == true
                                                   select new EmContact
                                                   {
                                                       EmContactId = a.EmContactId,
                                                       EmContactName = a.EmContactName,
                                                       Relationship = a.Relationship,
                                                       EmergencyContactPhone = a.EmergencyContactPhone,
                                                       CustomerId = a.CustomerId
                                                   });
                if (emContact == null)
                {
                    return NotFound();
                }

                return Ok(emContact);
            }

            if (token.Count() > 20)
            {
                IQueryable<EmContact> emContact = (from a in _context.EmContact
                                                   where a.CustomerId == id
                                                   select new EmContact
                                                   {
                                                       EmContactId = a.EmContactId,
                                                       EmContactName = a.EmContactName,
                                                       Relationship = a.Relationship,
                                                       EmergencyContactPhone = a.EmergencyContactPhone,
                                                       CustomerId = a.CustomerId,
                                                       ShowOnPublicView = a.ShowOnPublicView,
                                                       CustUserName = a.CustUserName
                                                   });
                if (emContact == null)
                {
                    return NotFound();
                }

                return Ok(emContact);
            }
            return Ok();
        }


        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]EmContact emContact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingEmContact = (from a in _context.EmContact
                                     where a.EmContactName == emContact.EmContactName
                                     && a.CustomerId == emContact.CustomerId
                                     select a);

            //if emContact exists, it won't create another
            if (existingEmContact.Count<EmContact>() > 0)
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }

            _context.EmContact.Add(emContact);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EmContactExists(emContact.EmContactId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            //return CreatedAtRoute("GetEmContact", new { id = emContact.EmContactId }, emContact);
            return Ok(emContact);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]EmContact emContact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != emContact.EmContactId)
            {
                return BadRequest();
            }

            _context.Entry(emContact).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmContactExists(emContact.EmContactId))
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

            EmContact emContact = _context.EmContact.Single(c => c.EmContactId == id);
            if (emContact == null)
            {
                return NotFound();
            }

            _context.EmContact.Remove(emContact);
            _context.SaveChanges();

            return Ok(emContact);
        }

        private bool EmContactExists(int id)
        {
            return _context.EmContact.Count(c => c.EmContactId == id) > 0;
        }
    }
}