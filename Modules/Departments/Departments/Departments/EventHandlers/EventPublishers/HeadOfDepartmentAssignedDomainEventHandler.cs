using Departments.Departments.Events;
using MassTransit;
using Shared.Messaging.Events;

namespace Departments.Departments.EventHandlers.EventPublishers
{

    public class HeadOfDepartmentAssignedDomainEventHandler(IBus bus) : INotificationHandler<HeadOfDepartmentAssignedDomainEvent>
    {
        public async Task Handle(HeadOfDepartmentAssignedDomainEvent notification, CancellationToken cancellationToken)
        {
            var integrationEvent = new HeadOfDepartmentAssignedIntegrationEvent()
            {
                DoctorId = notification.DoctorId,
            };
            await bus.Publish(integrationEvent);
        }
    }
}
