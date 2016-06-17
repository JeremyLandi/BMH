using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BasicMedicalHistory.Models
{
    public class MedicalCondition
    {
        [Key]
        public int MedicalConditionId {get;set;}
        public string MedicalConditionName { get; set; }
        public string Description { get; set; }
        public bool ShowOnPublicView { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
