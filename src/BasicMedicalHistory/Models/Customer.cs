using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BasicMedicalHistory.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        //public int EmContactId { get; set; }
        //public int MedicationId { get; set; }
        //public int AllergyId { get; set; }
        //public int ConditionId { get; set; }
        //public int PhysicianId { get; set; }
        //public int InsuranceId { get; set; }
        public int BloodTypeId { get; set; }

        public string CustomerName { get; set; }
        public string CustAddress { get; set; }
        public string CustCity { get; set; }
        public string CustState { get; set; }
        public string CustPhone { get; set; }
        [DataType(DataType.Date)]
        public string BirthDate { get; set; }
        public string Gender { get; set; }
        public string Hair { get; set; }
        public string EyeColor { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string QrCode { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }

        public ICollection<EmContact> CustEmContacts { get; set; }
        public ICollection<Medication> CustMedications { get; set; }
        public ICollection<Allergy> CustAllergies { get; set; }
        public ICollection<MedicalCondition> CustMedicalCondition { get; set; }
        public ICollection<Physician> CustPhysicians { get; set; }
        public ICollection<Insurance> CustInsurances { get; set; }
    }
}
