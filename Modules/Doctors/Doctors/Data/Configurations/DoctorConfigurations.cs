
namespace Doctors.Data.Configurations
{
    public class DoctorConfigurations : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Name).IsRequired();


            builder.HasOne(d => d.Specialization)
           .WithMany(s => s.Doctors)
           .HasForeignKey(d => d.SpecializationId); 
        }
    }
}
