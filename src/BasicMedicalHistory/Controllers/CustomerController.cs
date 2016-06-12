using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using BasicMedicalHistory.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Web.Http;

namespace BasicMedicalHistory.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [EnableCors("AllowDevelopmentEnvironment")]
    public class CustomerController : ApiController
    {

        private BmhContext _context;

        public CustomerController(BmhContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult GetCustomer([FromQuery]int? id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IQueryable<Customer> customer = (from c in _context.Customer
                                            where c.CustomerId == id
                                            select new Customer
                                            {
                                                //meta data
                                                CustomerId = c.CustomerId,
                                                CreatedDate = c.CreatedDate,
                                                QrCode = c.QrCode,

                                                //contact info
                                                CustName = c.CustName,
                                                CustAddress = c.CustAddress,
                                                CustCity = c.CustCity,
                                                CustState = c.CustState,
                                                CustPhone = c.CustPhone,
                                                CustEmail = c.CustEmail,
                                                
                                                //personal info
                                                BloodTypeId = c.BloodTypeId,
                                                BirthDate = c.BirthDate,
                                                Gender = c.Gender,
                                                Hair = c.Hair,
                                                EyeColor = c.EyeColor,
                                                Height = c.Height,
                                                Weight = c.Weight,
                                            });

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingCustomer = (from c in _context.Customer
                                where c.CustName == customer.CustName
                                select new Customer
                                {
                                    //meta data
                                    CustomerId = c.CustomerId,
                                    CreatedDate = c.CreatedDate,
                                    QrCode = c.QrCode,

                                    //contact info
                                    CustName = c.CustName,
                                    CustAddress = c.CustAddress,
                                    CustCity = c.CustCity,
                                    CustState = c.CustState,
                                    CustPhone = c.CustPhone,
                                    CustEmail = c.CustEmail,

                                    //personal info
                                    BloodTypeId = c.BloodTypeId,
                                    BirthDate = c.BirthDate,
                                    Gender = c.Gender,
                                    Hair = c.Hair,
                                    EyeColor = c.EyeColor,
                                    Height = c.Height,
                                    Weight = c.Weight,
                                });

            //if customer exists, it won't create another
            if (existingCustomer.Count<Customer>() > 0)
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }

            //
            _context.Customer.Add(customer);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CustomerExists(customer.CustomerId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            //return CreatedAtRoute("GetCustomer", new { id = customer.CustomerId }, customer);
            return Ok(customer);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(customer.CustomerId))
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

            Customer customer = _context.Customer.Single(c => c.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customer.Remove(customer);
            _context.SaveChanges();

            return Ok(customer);
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Count(c => c.CustomerId == id) > 0;
        }
    }
}