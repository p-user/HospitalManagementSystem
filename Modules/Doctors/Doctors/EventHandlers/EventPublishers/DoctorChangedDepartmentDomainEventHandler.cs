using Doctors.DomainEvents;
using MassTransit;
using Shared.Messaging.Events;

namespace Doctors.EventHandlers.EventPublishers
{
    public class DoctorChangedDepartmentDomainEventHandler(IBus bus) : INotificationHandler<DoctorChangedDepartmentDomainEvent>
    {

        public async Task Handle(DoctorChangedDepartmentDomainEvent notification, CancellationToken cancellationToken)
        {
            //from here you dispatch the event in other modules
            var IntegrationEvent = new DoctorChangedDepartmentIntegrationEvent
            {
                DepartmentId = notification.doctor.DepartmentId,
                DoctorId = notification.doctor.Id,

            };

            await bus.Publish(IntegrationEvent, cancellationToken);
        }
    }
}
