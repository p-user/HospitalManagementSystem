

using Shared.DDD;

namespace Doctors.DomainEvents
{
    public record  DoctorAddedToApplicationUsersDomainEvent(Doctor Doctor) : IDomainEvent;
    
}
