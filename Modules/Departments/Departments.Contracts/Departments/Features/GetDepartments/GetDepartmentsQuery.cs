
using Departments.Contracts.Departments.Dtos;
using MediatR;

namespace Departments.Contracts.Departments.Features.GetDepartments
{
    public record GetDepartmentsQuery : IRequest<GetDepartmentsQueryResponse>;
    public record GetDepartmentsQueryResponse(List<DepartmentDto> DepartmentDtos);


}
