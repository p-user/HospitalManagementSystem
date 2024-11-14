
using Doctors.Contracts.Doctors.Features.GetDoctorById;

namespace Departments.Departments.Features.AddDoctorToWorkingShift
{

    public record AddDoctorToWorkingShiftCommand(Guid ShiftId, Guid DoctorId) : IRequest<bool>;
    public class AddDoctorToWorkingShiftCommandHandler(DepartmentsDbContext departmentsDbContext, ISender sender) : IRequestHandler<AddDoctorToWorkingShiftCommand, bool>
    {
        public async Task<bool> Handle(AddDoctorToWorkingShiftCommand request, CancellationToken cancellationToken)
        {
            var shift = await departmentsDbContext.WorkingShifts.FindAsync(request.ShiftId, cancellationToken);
            if (shift is null)
            {
                throw new NotFoundException($"Shift with id {request.ShiftId} was not found!");
            }

            var doctor = await sender.Send(new GetDoctorByIdQuery(request.DoctorId));

            shift.AddDoctorToShift(request.DoctorId);
            departmentsDbContext.WorkingShifts.Update(shift);
            await departmentsDbContext.SaveChangesAsync(cancellationToken);

            return true;


        }
    }
}
