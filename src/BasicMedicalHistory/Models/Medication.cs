using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BasicMedicalHistory.Models
{
    public class Medication
    {
        [Key]
        public int MedicationId { get; set; }
        public string GenericName { get; set; }
        public string MedicalName { get; set; }
        public string Dosage { get; set; }
        public string Frequency { get; set; }
        public string Usage { get; set; }
        public string Notes { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
