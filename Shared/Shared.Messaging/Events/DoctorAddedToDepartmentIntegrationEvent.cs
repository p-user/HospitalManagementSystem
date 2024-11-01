
namespace Shared.Messaging.Events
{
    public record DoctorAddedToDepartmentIntegrationEvent : IntegrationEvent
    {
        public Guid DoctorId { get; set; }
        public Guid DepartmentId { get; set; }
    }
}
