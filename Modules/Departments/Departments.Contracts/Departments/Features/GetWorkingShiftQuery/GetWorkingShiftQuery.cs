
using Departments.Contracts.Departments.Dtos;
using MediatR;

namespace Departments.Contracts.Departments.Features.GetWorkingShiftQuery
{
    public record GetWorkingShiftQuery(Guid ShiftId) : IRequest<GetWorkingShiftQueryResponse>;
    public record GetWorkingShiftQueryResponse(WorkingShiftDto WorkingShiftDto);


}
