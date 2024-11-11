using Microsoft.EntityFrameworkCore;
using Patients.Patients.Entities;
using System.Reflection;

namespace Patients.Data
{
    public class PatientsDbContext : DbContext
    {
        public PatientsDbContext(DbContextOptions<PatientsDbContext> options) : base(options)
        {
        }

        public DbSet<Patient> Patients => Set<Patient>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("Patients");
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); 
            base.OnModelCreating(builder);
        }
    }
}
