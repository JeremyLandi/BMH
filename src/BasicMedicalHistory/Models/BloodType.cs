using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BasicMedicalHistory.Models
{
    public class BloodType
    {
        [Key]
        public int BloodTypeId { get; set; }
        public string BloodTypeName { get; set; }
    }
}
