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
    public class MedicationController : ApiController
    {
        private BmhContext _context;

        public MedicationController(BmhContext context)
        {
            _context = context;
        }

        // get medication by
        [HttpGet]
        public IActionResult GetMedication([FromQuery]int? id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IQueryable<Medication> medication = (from a in _context.Medication
                                                 where a.MedicationId == id
                                                 select new Medication
                                                {
                                                   MedicationId = a.MedicationId,
                                                   GenericName = a.GenericName,
                                                   BrandName = a.BrandName,
                                                   Dosage = a.Dosage,
                                                   SideEffects = a.SideEffects,
                                                   DrugInteractions = a.DrugInteractions
                                                });

            if (medication == null)
            {
                return NotFound();
            }

            return Ok(medication);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Medication medication)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingMedication = (from a in _context.Medication
                                     where a.BrandName == medication.BrandName
                                     select new Medication
                                     {
                                         MedicationId = a.MedicationId,
                                         GenericName = a.GenericName,
                                         BrandName = a.BrandName,
                                         Dosage = a.Dosage,
                                         SideEffects = a.SideEffects,
                                         DrugInteractions = a.DrugInteractions
                                     });

            //if medication exists, it won't create another
            if (existingMedication.Count<Medication>() > 0)
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }

            _context.Medication.Add(medication);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (MedicationExists(medication.MedicationId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            //return CreatedAtRoute("GetMedication", new { id = medication.MedicationId }, medication);
            return Ok(medication);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Medication medication)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != medication.MedicationId)
            {
                return BadRequest();
            }

            _context.Entry(medication).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicationExists(medication.MedicationId))
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

            Medication medication = _context.Medication.Single(c => c.MedicationId == id);
            if (medication == null)
            {
                return NotFound();
            }

            _context.Medication.Remove(medication);
            _context.SaveChanges();

            return Ok(medication);
        }

        private bool MedicationExists(int id)
        {
            return _context.Medication.Count(c => c.MedicationId == id) > 0;
        }
    }
}