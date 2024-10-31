
using Doctors.Events;

namespace Doctors.EventHandlers
{
    public class DoctorAddedToDepartmentHandler : INotificationHandler<DoctorAddedToDepartmentEvent>
    {
        public Task Handle(DoctorAddedToDepartmentEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
