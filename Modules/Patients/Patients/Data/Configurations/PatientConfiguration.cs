using Microsoft.EntityFrameworkCore;
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
            });
        }
    }
}
