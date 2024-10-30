

using Departments.Contracts.Departments.Dtos;
using MediatR;

namespace Departments.Contracts.Departments.Features.GetDepartment
{
    public record GetDepartmentByIdQuery(Guid Id): IRequest<GetDepartmentByIdQueryResponse>;
    public record GetDepartmentByIdQueryResponse(DepartmentDto DepartmentDto);
}
