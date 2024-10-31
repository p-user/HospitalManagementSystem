
using Shared.DDD;

namespace Doctors.Events
{
    public record DoctorChangedDepartmentEvent(Doctor doctor) : IDomainEvent;
    
}
