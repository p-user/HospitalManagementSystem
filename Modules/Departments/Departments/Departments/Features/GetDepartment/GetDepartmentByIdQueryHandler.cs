
namespace Departments.Departments.Features.GetDepartment
{
    public class GetDepartmentByIdQueryHandler(DepartmentsDbContext departmentsDbContext) : IRequestHandler<GetDepartmentByIdQuery, GetDepartmentByIdQueryResponse>
    {
        public async Task<GetDepartmentByIdQueryResponse> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await departmentsDbContext.Departments.SingleOrDefaultAsync(s=>s.Id== request.Id,cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException($" Department with id {request.Id} not found!");
            }
            var departmentDto = entity.Adapt<DepartmentDto>();
            return new GetDepartmentByIdQueryResponse(departmentDto);
        }
    }
}
