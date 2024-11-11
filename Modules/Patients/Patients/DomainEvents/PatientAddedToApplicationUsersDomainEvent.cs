using Shared.DDD;

namespace Patients.DomainEvents
{
    public record PatientAddedToApplicationUsersDomainEvent(string Email) : IDomainEvent;

}
