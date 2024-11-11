
using Departments.Departments.Entities;

namespace Departments.Departments.Features.AssignDoctorToDepartment
{
    public record AssignDoctorToDepartmentCommand(Guid DepartmentId, Guid DoctorId) : IRequest<AssignDoctorToDepartmentCommandResponse>;
    public record AssignDoctorToDepartmentCommandResponse(bool Succeded);
    public class AssignDoctorToDepartmentCommandHandler(DepartmentsDbContext departmentsDbContext) : IRequestHandler<AssignDoctorToDepartmentCommand, AssignDoctorToDepartmentCommandResponse>
    {
        public async Task<AssignDoctorToDepartmentCommandResponse> Handle(AssignDoctorToDepartmentCommand request, CancellationToken cancellationToken)
        {
            //get doctors previous department

            var previousDepartment = await departmentsDbContext.Departments.Where(s=>s.Doctors.Any(d=>d ==  request.DoctorId)).FirstOrDefaultAsync(cancellationToken);
            if (previousDepartment is not null)
            {
                //remove doctor from previous department

                RemoveDoctorFromDepartment(previousDepartment, request.DoctorId);
                departmentsDbContext.Departments.Update(previousDepartment);
                
            }


            //retrive the new department 
            var newDepartment = await departmentsDbContext.Departments.SingleOrDefaultAsync(s=>s.Id == request.DepartmentId,cancellationToken);
            if (newDepartment is null) 
            { 
                departmentsDbContext.ChangeTracker.Clear();
                throw new NotFoundException($" Department you provided is not valid!");

            }

            //add doctor to the new department 
            AddDoctorToDepartment(newDepartment, request.DoctorId);
            departmentsDbContext.Departments.Update(newDepartment);

            //save changes 
            await departmentsDbContext.SaveChangesAsync(cancellationToken);

            return new AssignDoctorToDepartmentCommandResponse(true);
        }

        private void AddDoctorToDepartment(Department department, Guid doctorId)
        {
            department.AddDoctorToDepartment(doctorId);
        }

        private void RemoveDoctorFromDepartment(Department department, Guid doctorId)
        {
            department.RemoveDoctorFromDepartment(doctorId);
        }
    }
}
