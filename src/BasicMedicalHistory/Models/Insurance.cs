using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BasicMedicalHistory.Models
{
    public class Insurance
    {
        [Key]
        public int InsuranceId { get; set; }
        public string InsuranceProvider { get; set; }
        public string IdNumber { get; set; }
        public string GroupNumber { get; set; }
        public string BIN { get; set; }
        [DataType(DataType.Currency)]
        public int Deducatable { get; set; }
        public string Phone { get; set; }
        public string Notes { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
