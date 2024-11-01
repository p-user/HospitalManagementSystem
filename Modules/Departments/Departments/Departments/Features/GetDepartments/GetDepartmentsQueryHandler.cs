
using Departments.Contracts.Departments.Features.GetDepartments;

namespace Departments.Departments.Features.GetDepartments
{
    public class GetDepartmentsQueryHandler(DepartmentsDbContext departmentsDbContext) : IRequestHandler<GetDepartmentsQuery, GetDepartmentsQueryResponse>
    {
        public async Task<GetDepartmentsQueryResponse> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var result = await departmentsDbContext.Departments.ToListAsync(cancellationToken);
            var dtos = result.Adapt<List<DepartmentDto>>();
            return new GetDepartmentsQueryResponse(dtos);
        }
    }
}
