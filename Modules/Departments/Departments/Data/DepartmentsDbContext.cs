using Departments.Departments.Entities;

namespace Departments.Data
{
    public class DepartmentsDbContext : DbContext
    {
        public DepartmentsDbContext(DbContextOptions<DepartmentsDbContext> options) : base(options)
        {
        }

        public DbSet<Department> Departments => Set<Department>();
        public DbSet<WorkingShift> WorkingShifts => Set<WorkingShift>();
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("Departments");
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); 
            base.OnModelCreating(builder);
        }
    }
}
