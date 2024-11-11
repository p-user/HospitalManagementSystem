using Shared.DDD;

namespace Doctors.DomainEvents
{
    public record DoctorAddedToDepartmentDomainEvent(Doctor Doctor) : IDomainEvent;

}
