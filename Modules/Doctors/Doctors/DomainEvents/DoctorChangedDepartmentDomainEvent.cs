using Shared.DDD;

namespace Doctors.DomainEvents
{
    public record DoctorChangedDepartmentDomainEvent(Doctor doctor) : IDomainEvent;

}
