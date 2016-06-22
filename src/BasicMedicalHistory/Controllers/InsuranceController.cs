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
    public class InsuranceController : ApiController
    {

        private BmhContext _context;

        public InsuranceController(BmhContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult GetInsurance([FromQuery]int? id, [FromQuery] string token, [FromQuery]string custUserName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (token == null)
            {
                IQueryable<Insurance> insurance = (from a in _context.Insurance
                                                   where a.CustUserName == custUserName
                                                   && a.ShowOnPublicView == false
                                                   select new Insurance
                                                   {
                                                       InsuranceId = a.InsuranceId,
                                                       InsuranceProvider = a.InsuranceProvider,
                                                       IdNumber = a.IdNumber,
                                                       GroupNumber = a.GroupNumber,
                                                       BIN = a.BIN,
                                                       Deducatable = a.Deducatable,
                                                       Phone = a.Phone,
                                                       Notes = a.Notes,
                                                       CustomerId = a.CustomerId
                                                   });

                if (insurance == null)
                {
                    return NotFound();
                }

                return Ok(insurance);
            }


            if (token.Count() > 20)
            {
                IQueryable<Insurance> insurance = (from a in _context.Insurance
                                                   where a.CustomerId == id
                                                   select new Insurance
                                                   {
                                                       InsuranceId = a.InsuranceId,
                                                       InsuranceProvider = a.InsuranceProvider,
                                                       IdNumber = a.IdNumber,
                                                       GroupNumber = a.GroupNumber,
                                                       BIN = a.BIN,
                                                       Deducatable = a.Deducatable,
                                                       Phone = a.Phone,
                                                       Notes = a.Notes,
                                                       CustomerId = a.CustomerId,
                                                       ShowOnPublicView = a.ShowOnPublicView,
                                                       CustUserName = a.CustUserName
                                                   });

                if (insurance == null)
                {
                    return NotFound();
                }

                return Ok(insurance);
            }

            return Ok();
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Insurance insurance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingInsurance = (from a in _context.Insurance
                                     where a.InsuranceProvider == insurance.InsuranceProvider
                                     && a.CustomerId == insurance.CustomerId
                                     select a);

            //if insurance exists, it won't create another
            if (existingInsurance.Count<Insurance>() > 0)
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }

            _context.Insurance.Add(insurance);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (InsuranceExists(insurance.InsuranceId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            //return CreatedAtRoute("GetInsurance", new { id = insurance.InsuranceId }, insurance);
            return Ok(insurance);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Insurance insurance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != insurance.InsuranceId)
            {
                return BadRequest();
            }

            _context.Entry(insurance).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InsuranceExists(insurance.InsuranceId))
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

            Insurance insurance = _context.Insurance.Single(c => c.InsuranceId == id);
            if (insurance == null)
            {
                return NotFound();
            }

            _context.Insurance.Remove(insurance);
            _context.SaveChanges();

            return Ok(insurance);
        }

        private bool InsuranceExists(int id)
        {
            return _context.Insurance.Count(c => c.InsuranceId == id) > 0;
        }
    }
}
