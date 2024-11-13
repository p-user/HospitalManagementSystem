using Appointments.Appointments.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


namespace Appointments.Data
{
    public class AppointmentsDbContext : DbContext
    {
        public AppointmentsDbContext(DbContextOptions<AppointmentsDbContext> options) : base(options)
        {
        }

        public DbSet<AvailableSlot> AvailableSlots => Set<AvailableSlot>();
        public DbSet<Appointment> Appointments => Set<Appointment>();
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("Appointments");
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); 
            base.OnModelCreating(builder);
        }
    }
}
