using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;
using Microsoft.AspNetCore.Cors;
using BasicMedicalHistory.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BasicMedicalHistory.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [EnableCors("AllowDevelopmentEnvironment")]
    public class AllergyController : ApiController
    {

        private BmhContext _context;

        public AllergyController(BmhContext context)
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

            IQueryable<Allergy> allergy = (from a in _context.Allergy
                                            where a.CustomerId == id
                                            select new Allergy
                                            {
                                                AllergyId = a.AllergyId,
                                                Name = a.Name,
                                                Reaction = a.Reaction,
                                                Notes = a.Notes,
                                                CustomerId = a.CustomerId
                                            });

            if (allergy == null)
            {
                return NotFound();
            }

            return Ok(allergy);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Allergy allergy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingAllergy = (from a in _context.Allergy
                                   where a.AllergyId == allergy.AllergyId 
                                   select new Allergy
                                   {
                                       AllergyId = a.AllergyId,
                                       Name = a.Name,
                                       Reaction = a.Reaction,
                                       Notes = a.Notes,
                                       CustomerId = a.CustomerId
                                   });

            //if allergy exists, it won't create another
            if (existingAllergy.Count<Allergy>() > 0)
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }

            
            _context.Allergy.Add(allergy);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AllergyExists(allergy.AllergyId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            //return CreatedAtRoute("GetAllergy", new { id = allergy.AllergyId }, allergy);
            return Ok(allergy);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Allergy allergy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != allergy.AllergyId)
            {
                return BadRequest();
            }

            _context.Entry(allergy).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AllergyExists(allergy.AllergyId))
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

            Allergy allergy = _context.Allergy.Single(c => c.AllergyId == id);
            if (allergy == null)
            {
                return NotFound();
            }

            _context.Allergy.Remove(allergy);
            _context.SaveChanges();

            return Ok(allergy);
        }

        private bool AllergyExists(int id)
        {
            return _context.Allergy.Count(c => c.AllergyId == id) > 0;
        }
    }
}
