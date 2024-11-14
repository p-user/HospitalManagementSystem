
using Departments.Departments.Events;
using MassTransit;
using Shared.Messaging.Events;

namespace Departments.Departments.EventHandlers.EventPublishers
{
    public class DoctorAddedToWorkingShiftDomainEventHandler(IBus bus) : INotificationHandler<DoctorAddedToWorkingShiftDomainEvent>
    {
        public async Task Handle(DoctorAddedToWorkingShiftDomainEvent notification, CancellationToken cancellationToken)
        {
            var integrationEvent = new DoctorAddedToWorkingShiftIntegrationEvent
            {
                ShiftId = notification.ShiftId,
                DoctorId = notification.DoctorId,
            };

            await bus.Publish(integrationEvent);
        }

    }
}
