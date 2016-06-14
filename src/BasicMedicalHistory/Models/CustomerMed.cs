using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BasicMedicalHistory.Models
{
    public class CustomerMed
    {
        [Key]
        public int CustomerMedId { get; set; }
        public int MedicationId { get; set; }
        public int CustomerId { get; set; }
        public string Usage { get; set; }
        public string Frequency { get; set; }
        public string Notes { get; set; }

        public Customer Customer { get; set; }
        public Medication Medication { get; set; }
    }
}
