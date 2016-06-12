using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BasicMedicalHistory.Models
{
    public class Allergy
    {
        [Key]
        public int AllergyId { get; set; }
        public string Name { get; set; }
        public string Reaction { get; set; }
        public string Notes { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
