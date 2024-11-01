
using Departments.Departments.Models;

namespace Departments.Departments.Features.CreateDepartment
{
    public record CreateDepartmentCommand(DepartmentDto DepartmentDto) : IRequest<CreateDepartmentCommandResponse>;
    public record CreateDepartmentCommandResponse(Guid Id);
    public class CreateDepartmentCommandHandler(DepartmentsDbContext departmentsDbContext) : IRequestHandler<CreateDepartmentCommand, CreateDepartmentCommandResponse>
    {
        public async  Task<CreateDepartmentCommandResponse> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            //add validation

            //create entity
            var entity = CreateDepartment(request.DepartmentDto);
            //add and savechanges
            await departmentsDbContext.Departments.AddAsync(entity);
            await departmentsDbContext.SaveChangesAsync(cancellationToken);

            return new CreateDepartmentCommandResponse(entity.Id);

        }

        private Department CreateDepartment(DepartmentDto departmentDto)
        {
            return Department.Create(departmentDto.Name, departmentDto.Description);
        }
    }
}
