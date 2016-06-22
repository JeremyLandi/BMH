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
        public IActionResult GetCustomerMed([FromQuery]int? id, [FromQuery] string token, [FromQuery]string custUserName)
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
        public IActionResult Post([FromBody]MedicationPostView medicationPostView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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
            #region existingMedication

            //if medication exists, it will use current to create new Customer Medication
            if (currentMedication.Count<Medication>() > 0)
            {
                // DONT NEED IF API WORKS CORRECTLY - will have correct once and can search by current ID
                //var currentMed = (from a in _context.Medication
                //              where a.MedicationId == medicationPostView.MedicationId
                //              select new Medication
                //              {
                //                  MedicationId = a.MedicationId,
                //                  GenericName = a.GenericName,
                //                  BrandName = a.BrandName,
                //                  Dosage = a.Dosage,
                //                  SideEffects = a.SideEffects,
                //                  DrugInteractions = a.DrugInteractions
                //              }).Last();

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
            #endregion

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

                var newMed = currentMedication.Last();


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

            if (id != medicationPostView.MedicationId)
            {
                return BadRequest();
            }

            CustomerMed customerMed = new CustomerMed
            {
                MedicationId = medicationPostView.MedicationId,
                CustomerId = medicationPostView.CustomerId,
                Usage = medicationPostView.Usage,
                Frequency = medicationPostView.Frequency,
                Notes = medicationPostView.Notes,
                ShowOnPublicView = medicationPostView.ShowOnPublicView,
                CustUserName = medicationPostView.CustUserName
            };
            _context.Entry(customerMed).State = EntityState.Modified;

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

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Cors;
//using System.Web.Http;
//using BasicMedicalHistory.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.EntityFrameworkCore;

//namespace BasicMedicalHistory.Controllers
//{
//    [Route("api/[controller]")]
//    [Produces("application/json")]
//    [EnableCors("AllowDevelopmentEnvironment")]
//    public class MedicationController : ApiController
//    {
//        private BmhContext _context;

//        public MedicationController(BmhContext context)
//        {
//            _context = context;
//        }

//        // get medication by
//        [HttpGet]
//        public IActionResult GetMedication([FromQuery]int? id)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            IQueryable<Medication> medication = (from a in _context.Medication
//                                                 where a.MedicationId == id
//                                                 select new Medication
//                                                 {
//                                                     MedicationId = a.MedicationId,
//                                                     GenericName = a.GenericName,
//                                                     BrandName = a.BrandName,
//                                                     Dosage = a.Dosage,
//                                                     SideEffects = a.SideEffects,
//                                                     DrugInteractions = a.DrugInteractions
//                                                 });

//            if (medication == null)
//            {
//                return NotFound();
//            }

//            return Ok(medication);
//        }

//        // POST api/values
//        [HttpPost]
//        public IActionResult Post([FromBody]MedicationPostView medicationPostView)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            var currentMedication = (from a in _context.Medication
//                                     where a.BrandName == medicationPostView.BrandName
//                                     && a.Dosage == medicationPostView.Dosage
//                                     select new Medication
//                                     {
//                                         MedicationId = a.MedicationId,
//                                         GenericName = a.GenericName,
//                                         BrandName = a.BrandName,
//                                         Dosage = a.Dosage,
//                                         SideEffects = a.SideEffects,
//                                         DrugInteractions = a.DrugInteractions
//                                     });
//            #region existingMedication

//            //if medication exists, it will use current to create new Customer Medication
//            if (currentMedication.Count<Medication>() > 0)
//            {
//                // DONT NEED IF API WORKS CORRECTLY - will have correct once and can search by current ID
//                //var currentMed = (from a in _context.Medication
//                //              where a.MedicationId == medicationPostView.MedicationId
//                //              select new Medication
//                //              {
//                //                  MedicationId = a.MedicationId,
//                //                  GenericName = a.GenericName,
//                //                  BrandName = a.BrandName,
//                //                  Dosage = a.Dosage,
//                //                  SideEffects = a.SideEffects,
//                //                  DrugInteractions = a.DrugInteractions
//                //              }).Last();

//                CustomerMed existingCustomerMed = new CustomerMed
//                {
//                    MedicationId = medicationPostView.MedicationId,
//                    CustomerId = medicationPostView.CustomerId,
//                    Usage = medicationPostView.Usage,
//                    Frequency = medicationPostView.Frequency,
//                    Notes = medicationPostView.Notes,
//                    ShowOnPublicView = medicationPostView.ShowOnPublicView,
//                    CustUserName = medicationPostView.CustUserName
//                };

//                _context.CustomerMed.Add(existingCustomerMed);
//                _context.SaveChanges();

//                return Ok(medicationPostView);
//            }
//            #endregion

//            else
//            {
//                Medication medication = new Medication
//                {
//                    GenericName = medicationPostView.GenericName,
//                    BrandName = medicationPostView.BrandName,
//                    Dosage = medicationPostView.Dosage,
//                    SideEffects = medicationPostView.SideEffects,
//                    DrugInteractions = medicationPostView.DrugInteractions
//                };

//                _context.Medication.Add(medication);
//                _context.SaveChanges();

//                var newMed = currentMedication.Last();


//                CustomerMed customerMed = new CustomerMed
//                {
//                    MedicationId = newMed.MedicationId,
//                    CustomerId = medicationPostView.CustomerId,
//                    Usage = medicationPostView.Usage,
//                    Frequency = medicationPostView.Frequency,
//                    Notes = medicationPostView.Notes,
//                    ShowOnPublicView = medicationPostView.ShowOnPublicView,
//                    CustUserName = medicationPostView.CustUserName
//                };

//                _context.CustomerMed.Add(customerMed);

//                try
//                {
//                    _context.SaveChanges();
//                }
//                catch (DbUpdateException)
//                {
//                    if (CustomerMedExists(customerMed.CustomerMedId))
//                    {
//                        return new StatusCodeResult(StatusCodes.Status409Conflict);
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }

//                return Ok(medicationPostView);
//            }
//        }

//        // PUT api/values/5
//        [HttpPut("{id}")]
//        public IActionResult Put(int id, [FromBody] MedicationPostView medicationPostView)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            if (id != medicationPostView.MedicationId)
//            {
//                return BadRequest();
//            }

//            CustomerMed customerMed = new CustomerMed
//            {
//                MedicationId = medicationPostView.MedicationId,
//                CustomerId = medicationPostView.CustomerId,
//                Usage = medicationPostView.Usage,
//                Frequency = medicationPostView.Frequency,
//                Notes = medicationPostView.Notes,
//                ShowOnPublicView = medicationPostView.ShowOnPublicView,
//                CustUserName = medicationPostView.CustUserName
//            };

//            select new Medication
//            {
//                MedicationId = a.MedicationId,
//                GenericName = a.GenericName,
//                BrandName = a.BrandName,
//                Dosage = a.Dosage,
//                SideEffects = a.SideEffects,
//                DrugInteractions = a.DrugInteractions
//            });


//            _context.Entry(medicationPostView).State = EntityState.Modified;

//            try
//            {
//                _context.SaveChanges();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!MedicationExists(medicationPostView.MedicationId))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return new StatusCodeResult(StatusCodes.Status204NoContent);
//        }

//        // DELETE api/values/5
//        [HttpDelete("{id}")]
//        public IActionResult Delete(int id)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            Medication medication = _context.Medication.Single(c => c.MedicationId == id);
//            if (medication == null)
//            {
//                return NotFound();
//            }

//            _context.Medication.Remove(medication);
//            _context.SaveChanges();

//            return Ok(medication);
//        }

//        private bool MedicationExists(int id)
//        {
//            return _context.Medication.Count(c => c.MedicationId == id) > 0;
//        }
//        private bool CustomerMedExists(int id)
//        {
//            return _context.CustomerMed.Count(c => c.CustomerMedId == id) > 0;
//        }

    
