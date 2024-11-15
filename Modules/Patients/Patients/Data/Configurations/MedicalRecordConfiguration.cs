
using MassTransit.Transports;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Patients.Patients.Entities;

namespace Patients.Data.Configurations
{
    public class MedicalRecordConfiguration : IEntityTypeConfiguration<MedicalRecord>
    {
        public void Configure(EntityTypeBuilder<MedicalRecord> builder)
        {
            builder.HasOne(p => p.Patient)
            .WithMany(m => m.MedicalRecords)
            .HasForeignKey(r => r.PatientId);

           
        }
    }
}
