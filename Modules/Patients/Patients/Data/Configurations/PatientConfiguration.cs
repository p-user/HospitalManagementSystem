using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Patients.Patients.Entities;

namespace Patients.Data.Configurations
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.OwnsMany(p => p.Allergies, a =>
            {
                a.Property(al => al.AllergyName).IsRequired();
                a.Property(al => al.AllergyType).IsRequired();
                a.Property(al => al.Reaction).IsRequired();
                a.Property(al => al.DateReported).IsRequired();
            })

            .Navigation(p => p.Allergies)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasField("_allergies");

            builder.HasMany(o => o.MedicalRecords)
            .WithOne()
            .HasForeignKey(o => o.Id);

            builder.Navigation(o => o.MedicalRecords)
            .UsePropertyAccessMode(PropertyAccessMode.Field).HasField("_medicalRecords");
        }
    }
}
