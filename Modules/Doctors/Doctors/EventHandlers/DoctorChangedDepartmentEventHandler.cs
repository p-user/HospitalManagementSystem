using Doctors.Events;

namespace Doctors.EventHandlers
{
    public class DoctorChangedDepartmentEventHandler : INotificationHandler<DoctorChangedDepartmentEvent>
    {
       
        public Task Handle(DoctorChangedDepartmentEvent notification, CancellationToken cancellationToken)
        {
            //from here you dispatch the event in other modules
            throw new NotImplementedException();

            //use rabbitmq
        }
    }
}
