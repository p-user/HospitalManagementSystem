using Departments.Departments.Models;
using Doctors.Contracts.Doctors.Features.GetDoctorById;

namespace Departments.Departments.Features.AssignHeadOfDepartment
{

    public record AssignHeadOfDepartmentCommand(Guid DepartmentId,Guid DoctorId) : IRequest<AssignHeadOfDepartmentCommandResponse>;
    public record AssignHeadOfDepartmentCommandResponse(bool Succeded);
    public class AssignHeadOfDepartmentCommandHandler(DepartmentsDbContext departmentsDbContext, ISender sender) : IRequestHandler<AssignHeadOfDepartmentCommand, AssignHeadOfDepartmentCommandResponse>
    {
        public async Task<AssignHeadOfDepartmentCommandResponse> Handle(AssignHeadOfDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await departmentsDbContext.Departments.SingleOrDefaultAsync(s=>s.Id==request.DepartmentId,cancellationToken);

            if (department == null) 
            {
                throw new NotFoundException($"Department with id {request.DepartmentId} was not found");
            }

            var doctor = await sender.Send(new GetDoctorByIdQuery(request.DoctorId));

            AssignHeadOfDepartment(department, request.DoctorId);

            departmentsDbContext.Departments.Update(department);
            await departmentsDbContext.SaveChangesAsync(cancellationToken);

            return new AssignHeadOfDepartmentCommandResponse(true);
        }

        private void AssignHeadOfDepartment(Department department, Guid doctorId)
        {
            department.AssignHeadOfDepartment(doctorId);
        }
    }
}
