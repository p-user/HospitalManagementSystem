
namespace Shared.Messaging.Events
{
    public record DoctorChangedDepartmentIntegrationEvent : IntegrationEvent
    {
        public Guid DoctorId { get; set; } = default!;
        public Guid DepartmentId { get; set; } = default!;
    }
}
