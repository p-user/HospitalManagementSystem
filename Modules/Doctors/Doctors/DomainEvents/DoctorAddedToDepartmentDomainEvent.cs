
using Shared.DDD;

namespace Doctors.Events
{
    public record DoctorAddedToDepartmentDomainEvent(Doctor Doctor) : IDomainEvent;
    
}
