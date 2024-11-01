
using Shared.DDD;

namespace Doctors.Events
{
    public record DoctorChangedDepartmentDomainEvent(Doctor doctor) : IDomainEvent;
    
}
