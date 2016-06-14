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
        public string BrandName { get; set; }
        public string Dosage { get; set; }
        public string SideEffects { get; set; }
        public string DrugInteractions { get; set; }

        public ICollection<CustomerMed> CustomerMed { get; set; }
    }
}
