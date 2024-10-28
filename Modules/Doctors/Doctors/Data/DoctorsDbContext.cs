namespace Doctors.Data
{
    public class DoctorsDbContext : DbContext
    {
        public DoctorsDbContext(DbContextOptions<DoctorsDbContext> options) : base(options)
        {

            
        }

        public DbSet<Doctor> Doctors => Set<Doctor>();
        public DbSet<Specialization> Specializations => Set<Specialization>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("Doctors");
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); //toDo set a specific class for model validation
            base.OnModelCreating(builder);
        }
    }
}
