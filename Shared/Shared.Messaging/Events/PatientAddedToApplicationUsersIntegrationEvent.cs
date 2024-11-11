

namespace Shared.Messaging.Events
{
    public record PatientAddedToApplicationUsersIntegrationEvent : IntegrationEvent
    {
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
