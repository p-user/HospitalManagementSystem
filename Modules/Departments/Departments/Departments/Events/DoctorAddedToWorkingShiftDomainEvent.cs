
using Shared.DDD;

namespace Departments.Departments.Events
{
    public record DoctorAddedToWorkingShiftDomainEvent(Guid ShiftId, Guid DoctorId) : IDomainEvent;
    
}
