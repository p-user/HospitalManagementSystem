
namespace Doctors.Data.Configurations
{
    public class SpecializationConfigurations : IEntityTypeConfiguration<Specialization>
    {
        public void Configure(EntityTypeBuilder<Specialization> builder)
        {
            builder.HasKey(s=>s.Id);

            builder.HasIndex(e => e.Name)
                  .IsUnique();

            builder.Property(s=>s.Name)
                .IsRequired();

            builder.Property(e => e.Description)
                  .IsRequired()
                  .HasMaxLength(1000);
        }
    }
}
