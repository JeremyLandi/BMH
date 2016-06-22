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
    public class MedicalConditionController : ApiController
    {
        private BmhContext _context;

        public MedicalConditionController(BmhContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult GetAllergy([FromQuery]int? id, [FromQuery] string token, [FromQuery]string custUserName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (token == null)
            {
                IQueryable<MedicalCondition> medicalCondition = (from a in _context.MedicalCondition
                                                                 where a.CustUserName == custUserName
                                                                 && a.ShowOnPublicView == false
                                                                 select new MedicalCondition
                                                                 {
                                                                     MedicalConditionId = a.MedicalConditionId,
                                                                     MedicalConditionName = a.MedicalConditionName,
                                                                     Description = a.Description,
                                                                     CustomerId = a.CustomerId,
                                                                     CustUserName = a.CustUserName
                                                                 });

                if (medicalCondition == null)
                {
                    return NotFound();
                }

                return Ok(medicalCondition);
            }

            if (token.Count() > 20)
            {
                IQueryable<MedicalCondition> medicalCondition = (from a in _context.MedicalCondition
                                                                 where a.CustomerId == id
                                                                 select new MedicalCondition
                                                                 {
                                                                     MedicalConditionId = a.MedicalConditionId,
                                                                     MedicalConditionName = a.MedicalConditionName,
                                                                     Description = a.Description,
                                                                     CustomerId = a.CustomerId,
                                                                     ShowOnPublicView = a.ShowOnPublicView,
                                                                     CustUserName = a.CustUserName
                                                                 });

                if (medicalCondition == null)
                {
                    return NotFound();
                }

                return Ok(medicalCondition);
            }

            return Ok();

        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]MedicalCondition medicalCondition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingMedicalCondition = (from a in _context.MedicalCondition
                                            where a.CustomerId == medicalCondition.CustomerId
                                            && a.MedicalConditionName == medicalCondition.MedicalConditionName
                                            select a);

            //if medicalCondition exists, it won't create another
            if (existingMedicalCondition.Count<MedicalCondition>() > 0)
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }

            _context.MedicalCondition.Add(medicalCondition);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (MedicalConditionExists(medicalCondition.MedicalConditionId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            //return CreatedAtRoute("GetMedicalCondition", new { id = medicalCondition.MedicalConditionId }, medicalCondition);
            return Ok(medicalCondition);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]MedicalCondition medicalCondition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != medicalCondition.MedicalConditionId)
            {
                return BadRequest();
            }

            _context.Entry(medicalCondition).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicalConditionExists(medicalCondition.MedicalConditionId))
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

            MedicalCondition medicalCondition = _context.MedicalCondition.Single(c => c.MedicalConditionId == id);
            if (medicalCondition == null)
            {
                return NotFound();
            }

            _context.MedicalCondition.Remove(medicalCondition);
            _context.SaveChanges();

            return Ok(medicalCondition);
        }

        private bool MedicalConditionExists(int id)
        {
            return _context.MedicalCondition.Count(c => c.MedicalConditionId == id) > 0;
        }
    }
}