using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BasicMedicalHistory.Models
{
    public class Physician
    {
        [Key]
        public int PhysicianId { get; set; }
        public string PhysicianName { get; set; }
        public string Title { get; set; }
        public string BusinessName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public bool ShowOnPublicView { get; set; }

        public int CustomerId { get; set; }
        public string CustUserName { get; set; }
        public Customer Customer { get; set; }
    }
}
