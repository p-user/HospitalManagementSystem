
using Departments.Departments.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Departments.Data.Configurations
{
    public class WorkingShiftConfiguration : IEntityTypeConfiguration<WorkingShift>
    {
        public void Configure(EntityTypeBuilder<WorkingShift> builder)
        {
            builder.HasOne(s=>s.Department)
                .WithMany(d=>d.WorkingShifts)
                .HasForeignKey(d => d.DepartmentId);
        }
    }
}
