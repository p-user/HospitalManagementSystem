using Doctors.Events;
using MassTransit;
using Shared.Messaging.Events;

namespace Doctors.EventHandlers.EventPublishers
{
    public class DoctorAddedToDepartmentDomainEventHandler(IBus bus) : INotificationHandler<DoctorAddedToDepartmentDomainEvent>
    {
        public async  Task Handle(DoctorAddedToDepartmentDomainEvent notification, CancellationToken cancellationToken)
        {
            var doctorId = notification.Doctor.Id;
            var departmentId = notification.Doctor.DepartmentId;

            var integrationEvent = new DoctorAddedToDepartmentIntegrationEvent
            {
                DoctorId = doctorId,
                DepartmentId = departmentId,
            };

            await bus.Publish(integrationEvent);


        }
    }
}
