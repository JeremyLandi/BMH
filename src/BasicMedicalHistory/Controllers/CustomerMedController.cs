using System.Collections.Generic;
using System.Linq;
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

        [HttpGet]
        public IActionResult GetCustomerMed([FromQuery]int? id, [FromQuery] string token, [FromQuery]string custUserName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //checks if they are logged in
            if (token == null)
            {
                IQueryable<MedicationPostView> medicationPostView = (from a in _context.Medication
                                                                     join b in _context.CustomerMed
                                                                     on a.MedicationId equals b.MedicationId
                                                                     where b.CustUserName == custUserName
                                                                     && b.ShowOnPublicView == true
                                                                     select new MedicationPostView
                                                                     {
                                                                         MedicationId = a.MedicationId,
                                                                         GenericName = a.GenericName,
                                                                         BrandName = a.BrandName,
                                                                         Dosage = a.Dosage,
                                                                         SideEffects = a.SideEffects,
                                                                         DrugInteractions = a.DrugInteractions,
                                                                         CustomerMedId = b.CustomerMedId,
                                                                         CustomerId = b.CustomerId,
                                                                         CustUserName = b.CustUserName,
                                                                         Usage = b.Usage,
                                                                         Frequency = b.Frequency,
                                                                         Notes = b.Notes,
                                                                         ShowOnPublicView = b.ShowOnPublicView
                                                                     });
                if (medicationPostView == null)
                {
                    return NotFound();
                }
                return Ok(medicationPostView);
            }

            if (token.Count() > 20)
            {
                IQueryable<MedicationPostView> medicationPostView = (from a in _context.Medication
                                                                     join b in _context.CustomerMed
                                                                     on a.MedicationId equals b.MedicationId
                                                                     where b.CustomerId == id
                                                                     select new MedicationPostView
                                                                     {
                                                                         MedicationId = a.MedicationId,
                                                                         GenericName = a.GenericName,
                                                                         BrandName = a.BrandName,
                                                                         Dosage = a.Dosage,
                                                                         SideEffects = a.SideEffects,
                                                                         DrugInteractions = a.DrugInteractions,
                                                                         CustomerMedId = b.CustomerMedId,
                                                                         CustomerId = b.CustomerId,
                                                                         CustUserName = b.CustUserName,
                                                                         Usage = b.Usage,
                                                                         Frequency = b.Frequency,
                                                                         Notes = b.Notes,
                                                                         ShowOnPublicView = b.ShowOnPublicView
                                                                     });
                if (medicationPostView == null)
                {
                    return NotFound();
                }
                return Ok(medicationPostView);
            }
            return Ok();
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]MedicationPostView medicationPostView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // checks to see if current drug exists
            var currentMedication = (from a in _context.Medication
                                     where a.BrandName == medicationPostView.BrandName
                                     && a.Dosage == medicationPostView.Dosage
                                     select new Medication
                                     {
                                         MedicationId = a.MedicationId,
                                         GenericName = a.GenericName,
                                         BrandName = a.BrandName,
                                         Dosage = a.Dosage,
                                         SideEffects = a.SideEffects,
                                         DrugInteractions = a.DrugInteractions
                                     });

            //if medication exists, it will use current drug to create new Customer Medication
            if (currentMedication.Count<Medication>() > 0)
            {
                CustomerMed existingCustomerMed = new CustomerMed
                {
                    MedicationId = medicationPostView.MedicationId,
                    CustomerId = medicationPostView.CustomerId,
                    Usage = medicationPostView.Usage,
                    Frequency = medicationPostView.Frequency,
                    Notes = medicationPostView.Notes,
                    ShowOnPublicView = medicationPostView.ShowOnPublicView,
                    CustUserName = medicationPostView.CustUserName
                };

                _context.CustomerMed.Add(existingCustomerMed);
                _context.SaveChanges();

                return Ok(medicationPostView);
            }

            //otherwise create new medication           
            else
            {
                Medication medication = new Medication
                {
                    GenericName = medicationPostView.GenericName,
                    BrandName = medicationPostView.BrandName,
                    Dosage = medicationPostView.Dosage,
                    SideEffects = medicationPostView.SideEffects,
                    DrugInteractions = medicationPostView.DrugInteractions
                };

                _context.Medication.Add(medication);
                _context.SaveChanges();

                //get last medication added
                var newMed = currentMedication.Last();

                //use that medication ID to create new customerMed
                CustomerMed customerMed = new CustomerMed
                {
                    MedicationId = newMed.MedicationId,
                    CustomerId = medicationPostView.CustomerId,
                    Usage = medicationPostView.Usage,
                    Frequency = medicationPostView.Frequency,
                    Notes = medicationPostView.Notes,
                    ShowOnPublicView = medicationPostView.ShowOnPublicView,
                    CustUserName = medicationPostView.CustUserName
                };

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
                return Ok(medicationPostView);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] MedicationPostView medicationPostView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CustomerMed customerMed = new CustomerMed
            {
                CustomerMedId = medicationPostView.CustomerMedId,
                MedicationId = medicationPostView.MedicationId,
                CustomerId = medicationPostView.CustomerId,
                Usage = medicationPostView.Usage,
                Frequency = medicationPostView.Frequency,
                Notes = medicationPostView.Notes,
                ShowOnPublicView = medicationPostView.ShowOnPublicView,
                CustUserName = medicationPostView.CustUserName
            };

            Medication medication = new Medication
            {
                MedicationId = medicationPostView.MedicationId,
                GenericName = medicationPostView.GenericName,
                BrandName = medicationPostView.BrandName,
                Dosage = medicationPostView.Dosage,
                SideEffects = medicationPostView.SideEffects,
                DrugInteractions = medicationPostView.DrugInteractions
            };

            _context.Entry(medication).State = EntityState.Modified;
            _context.Entry(customerMed).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicationExists(medicationPostView.MedicationId))
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

        private bool MedicationExists(int id)
        {
            return _context.Medication.Count(c => c.MedicationId == id) > 0;
        }
        private bool CustomerMedExists(int id)
        {
            return _context.CustomerMed.Count(c => c.CustomerMedId == id) > 0;
        }
    }
}