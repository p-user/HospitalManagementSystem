
using Shared.DDD;

namespace Departments.Departments.Events
{
    public record HeadOfDepartmentAssignedDomainEvent(Guid DoctorId) : IDomainEvent;


}
