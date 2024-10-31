
using Shared.DDD;

namespace Doctors.Events
{
    public record DoctorAddedToDepartmentEvent(Doctor Doctor) : IDomainEvent;
    
}
