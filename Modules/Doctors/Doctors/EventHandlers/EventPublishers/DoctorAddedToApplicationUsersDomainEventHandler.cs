using Doctors.DomainEvents;
using Doctors.Events;
using MassTransit;
using Shared.Constants;
using Shared.Messaging.Events;

namespace Doctors.EventHandlers.EventPublishers
{
    public class DoctorAddedToApplicationUsersDomainEventHandler(IBus bus) : INotificationHandler<DoctorAddedToApplicationUsersDomainEvent>
    {
        public async Task Handle(DoctorAddedToApplicationUsersDomainEvent notification, CancellationToken cancellationToken)
        {
            var integrationEvent = new DoctorAddedToApplicationUsersIntegrationEvent
            {
                Email = notification.Doctor.Email,
                Role = DefaultRoles.DoctorRole,
            };

            await bus.Publish(integrationEvent);
        }
    }
}
