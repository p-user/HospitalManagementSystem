

namespace Shared.Messaging.Events
{
    public record DoctorAddedToWorkingShiftIntegrationEvent : IntegrationEvent
    {
        public Guid DoctorId { get; init; }
        public Guid ShiftId { get; init; }
    }
}
