namespace Shared.Messaging.Events
{
    public record HeadOfDepartmentAssignedIntegrationEvent : IntegrationEvent
    {
        public Guid DoctorId { get; set; } = default!;
    }
    
}
