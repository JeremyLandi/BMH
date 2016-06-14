using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicMedicalHistory.Models
{
    public class BmhContext : DbContext
    {
        public BmhContext(DbContextOptions<BmhContext> options)
            : base(options)
        { }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<EmContact> EmContact { get; set; }
        public DbSet<MedicalCondition> MedicalCondition { get; set; }
        public DbSet<Allergy> Allergy { get; set; }
        public DbSet<Medication> Medication { get; set; }
        public DbSet<Insurance> Insurance { get; set; }
        public DbSet<Physician> Physician { get; set; }
        public DbSet<CustomerMed> CustomerMed { get; set; }
    }
}
