
using Departments.Contracts.Departments.Features.GetWorkingShiftQuery;

namespace Departments.Departments.Features.GetWorkingShift
{
    public class GetWorkingShiftQueryHandler(DepartmentsDbContext departmentsDbContext) : IRequestHandler<GetWorkingShiftQuery, GetWorkingShiftQueryResponse>
    {
        public async Task<GetWorkingShiftQueryResponse> Handle(GetWorkingShiftQuery request, CancellationToken cancellationToken)
        {
           var shift = await departmentsDbContext.WorkingShifts.Where(s=>s.Id==request.ShiftId).ToListAsync(cancellationToken);

            if (shift is null) 
            {
                throw new NotFoundException($"Working shift with id {request.ShiftId} was not found! ");
            }

            var mapped = shift.Adapt<WorkingShiftDto>();

            return new GetWorkingShiftQueryResponse(mapped);
        }
    }
}
