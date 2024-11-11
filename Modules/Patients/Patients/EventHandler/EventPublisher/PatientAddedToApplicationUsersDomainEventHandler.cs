
using MassTransit;
using MediatR;
using Patients.DomainEvents;
using Shared.Constants;
using Shared.Messaging.Events;

namespace Patients.EventHandler.EventPublisher
{
    public class PatientAddedToApplicationUsersDomainEventHandler(IBus bus) : INotificationHandler<PatientAddedToApplicationUsersDomainEvent>
    {
        public async Task Handle(PatientAddedToApplicationUsersDomainEvent notification, CancellationToken cancellationToken)
        {
            var integrationEvent = new PatientAddedToApplicationUsersIntegrationEvent
            {
                Email = notification.Email,
                Role = DefaultRoles.PatientRole,
            };

            await bus.Publish(integrationEvent);
        }
    }
}
