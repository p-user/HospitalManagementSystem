using Departments.Departments.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Departments.Data.Configurations
{
    public class DepartmentsConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasIndex(e => e.Name)
                   .IsUnique();

            builder.Property(e => e.Description)
                   .IsRequired()
                   .HasMaxLength(1000);

            //builder.HasMany(s => s.Doctors)
            //   .WithOne()
            //   .HasForeignKey(si => si.DepartmentId);
        }
    }
}
