using System.ComponentModel.DataAnnotations;


namespace Appointments.Appointments.Dtos
{
    public record AvailableSlotDto
    {
        [Required]
        public Guid DoctorId { get; init; }

        [Required]
        public Guid ShiftId { get; init; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; init; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; init; }

        [Required]
        public SlotStatus Status { get; init; }

        [Range(1, int.MaxValue, ErrorMessage = "Appointment limit must be greater than 0.")]
        public int AppointmentLimit { get; init; }
    }

    public enum SlotStatus
    {
        Available,
        Booked,
        Canceled
    }
}
