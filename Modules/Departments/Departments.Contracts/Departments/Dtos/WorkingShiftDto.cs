
using System.ComponentModel.DataAnnotations;

namespace Departments.Contracts.Departments.Dtos
{
    public record WorkingShiftDto
    {
        [Required]
        public ShiftName ShiftName { get; init; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; init; }

        [Required]
        public Guid DepartmentId { get; init; }

        
    }

    public enum ShiftName
    {
        Morning,
        Night
    }
}
