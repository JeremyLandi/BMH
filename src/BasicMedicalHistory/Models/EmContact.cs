using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BasicMedicalHistory.Models
{
    public class EmContact
    {
        [Key]
        public int EmContactId { get; set; }
        public string EmContactName { get; set; }
        public string Relationship { get; set; }
        public string EmergencyContactPhone { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
