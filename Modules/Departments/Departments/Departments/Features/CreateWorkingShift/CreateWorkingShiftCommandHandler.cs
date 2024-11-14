using Departments.Departments.Entities;
using Departments.Departments.Validators;

namespace Departments.Departments.Features.CreateWorkingShift
{
    public record CreateWorkingShiftCommand(WorkingShiftDto dto) : IRequest<bool>;
    public class CreateWorkingShiftCommandHandler(DepartmentsDbContext departmentsDbContext) : IRequestHandler<CreateWorkingShiftCommand, bool>
    {
        public async Task<bool> Handle(CreateWorkingShiftCommand request, CancellationToken cancellationToken)
        {
            // validate dto
            var validator = new WorkingShiftDtoValidator(departmentsDbContext);
            var validationResult = await validator.ValidateAsync(request.dto, cancellationToken);

            if (validationResult.IsValid)
            {
                var entity = CreateWorkingShift(request.dto);
                departmentsDbContext.WorkingShifts.Add(entity);
                await departmentsDbContext.SaveChangesAsync(cancellationToken);

                return true;
            }
            else 
            {
                var error = validationResult.Errors.FirstOrDefault();
                throw new Exception(error.ErrorMessage);
            }
        }

        private WorkingShift CreateWorkingShift(WorkingShiftDto request)
        {
            return  WorkingShift.Create(request.DepartmentId, (Entities.ShiftName)request.ShiftName, request.Date);
        }
    }
}
