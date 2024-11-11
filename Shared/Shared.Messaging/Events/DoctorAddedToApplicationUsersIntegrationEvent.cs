

namespace Shared.Messaging.Events
{
    public record DoctorAddedToApplicationUsersIntegrationEvent : IntegrationEvent
    {
        public string Role { get; set; }
        public string Email { get; set; }
    }
}
