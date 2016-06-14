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
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
        public string QrCode { get; set; }

        //contact info
        public string CustName { get; set; }
        public string CustAddress { get; set; }
        public string CustCity { get; set; }
        public string CustState { get; set; }
        public string CustPhone { get; set; }
        public string CustEmail { get; set; }

        //personal info
        public int BloodType { get; set; }
        [DataType(DataType.Date)]
        public string BirthDate { get; set; }
        public string Gender { get; set; }
        public string Hair { get; set; }
        public string EyeColor { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
       
        public ICollection<EmContact> EmContact { get; set; }
        public ICollection<CustomerMed> CustomerMed { get; set; }
        public ICollection<Allergy> Allergy { get; set; }
        public ICollection<MedicalCondition> MedicalCondition { get; set; }
        public ICollection<Physician> Physician { get; set; }
        public ICollection<Insurance> Insurance { get; set; }
    }
}