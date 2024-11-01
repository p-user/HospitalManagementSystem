

using Shared.Exceptions;

namespace Doctors.Doctors.Features.SetHeadOfDepartment
{
    public record SetHeadOfDepartmentCommand(Guid DoctorId) : IRequest<SetHeadOfDepartmentCommandResponse>;
    public record SetHeadOfDepartmentCommandResponse(bool Succeded);
    public class SetHeadOfDepartmentCommandHandler(DoctorsDbContext doctorsDbContext) : IRequestHandler<SetHeadOfDepartmentCommand, SetHeadOfDepartmentCommandResponse>
    {
        public async Task<SetHeadOfDepartmentCommandResponse> Handle(SetHeadOfDepartmentCommand request, CancellationToken cancellationToken)
        {
           var doctor = await doctorsDbContext.Doctors.SingleOrDefaultAsync(s=>s.Id == request.DoctorId);
            if (doctor == null) { throw new NotFoundException($"Doctor with id {request.DoctorId} not found!"); }

            SetHeadOfDepartment(doctor, true);
            doctorsDbContext.Doctors.Update(doctor);
            await doctorsDbContext.SaveChangesAsync(cancellationToken);

            return new SetHeadOfDepartmentCommandResponse(true);

        }

        private void SetHeadOfDepartment(Doctor doctor, bool isHeadOfDepartment)
        {
           doctor.SetHeadOfDepartment(isHeadOfDepartment);
        }
    }
}
