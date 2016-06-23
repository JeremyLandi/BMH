using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicMedicalHistory.Models
{
    public class MedicationPostView
    {
        
        public int CustomerMedId { get; set; }
        public int CustomerId { get; set; }
        public string CustUserName { get; set; }
        public string Usage { get; set; }
        public string Frequency { get; set; }
        public string Notes { get; set; }
        public bool ShowOnPublicView { get; set; }

        public int MedicationId { get; set; }
        public string GenericName { get; set; }
        public string BrandName { get; set; }
        public string Dosage { get; set; }
        public string SideEffects { get; set; }
        public string DrugInteractions { get; set; }
    }
}
