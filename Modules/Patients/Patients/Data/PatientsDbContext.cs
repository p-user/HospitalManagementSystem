using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Patients.Data
{
    public class PatientsDbContext : DbContext
    {
        public PatientsDbContext(DbContextOptions<PatientsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("Patients");
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); //toDo set a specific class for model validation
            base.OnModelCreating(builder);
        }
    }
}
