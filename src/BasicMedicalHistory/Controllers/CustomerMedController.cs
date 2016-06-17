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
    public class CustomerMedController : ApiController
    {
        private BmhContext _context;

        public CustomerMedController(BmhContext context)
        {
            _context = context;
        }

        // get customerMed by
        [HttpGet]
        public IActionResult GetCustomerMed([FromQuery]int? id, [FromQuery]bool showOnPublicView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (showOnPublicView == false)
            {
                IQueryable<CustomerMed> customerMed = (from a in _context.CustomerMed
                                                       where a.CustomerMedId == id
                                                       select new CustomerMed
                                                       {
                                                           CustomerMedId = a.CustomerMedId,
                                                           MedicationId = a.MedicationId,
                                                           CustomerId = a.CustomerId,
                                                           Usage = a.Usage,
                                                           Frequency = a.Frequency,
                                                           Notes = a.Notes
                                                       });
                if (customerMed == null)
                {
                    return NotFound();
                }

                return Ok(customerMed);
            }

            if (showOnPublicView)
            {
                IQueryable<CustomerMed> customerMed = (from a in _context.CustomerMed
                                                       where a.CustomerMedId == id
                                                       && a.ShowOnPublicView == showOnPublicView
                                                       select new CustomerMed
                                                       {
                                                           CustomerMedId = a.CustomerMedId,
                                                           MedicationId = a.MedicationId,
                                                           CustomerId = a.CustomerId,
                                                           Usage = a.Usage,
                                                           Frequency = a.Frequency,
                                                           Notes = a.Notes
                                                       });
                if (customerMed == null)
                {
                    return NotFound();
                }

                return Ok(customerMed);
            }

            return Ok();
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]CustomerMed customerMed)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingCustomerMed = (from a in _context.CustomerMed
                                      where a.MedicationId == customerMed.MedicationId
                                      && a.CustomerId == customerMed.CustomerId
                                      select a);

            //if customerMed exists, it won't create another
            if (existingCustomerMed.Count<CustomerMed>() > 0)
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }

            _context.CustomerMed.Add(customerMed);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CustomerMedExists(customerMed.CustomerMedId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            //return CreatedAtRoute("GetCustomerMed", new { id = customerMed.CustomerMedId }, customerMed);
            return Ok(customerMed);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]CustomerMed customerMed)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customerMed.CustomerMedId)
            {
                return BadRequest();
            }

            _context.Entry(customerMed).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerMedExists(customerMed.CustomerMedId))
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

            CustomerMed customerMed = _context.CustomerMed.Single(c => c.CustomerMedId == id);
            if (customerMed == null)
            {
                return NotFound();
            }

            _context.CustomerMed.Remove(customerMed);
            _context.SaveChanges();

            return Ok(customerMed);
        }

        private bool CustomerMedExists(int id)
        {
            return _context.CustomerMed.Count(c => c.CustomerMedId == id) > 0;
        }
    }
}