
using Departments.Data;
using FluentValidation;

namespace Departments.Departments.Validators
{
    public class WorkingShiftDtoValidator : AbstractValidator<WorkingShiftDto>
    {
        private readonly DepartmentsDbContext _departmentsDbContext;
        public WorkingShiftDtoValidator(DepartmentsDbContext departmentsDbContext)
        {
            _departmentsDbContext = departmentsDbContext;

            RuleFor(s=>s.DepartmentId).NotEmpty();
            RuleFor(s=>s.DepartmentId).MustAsync(Exist);
        }

        private async Task<bool> Exist(Guid guid, CancellationToken token)
        {
            var entity = await _departmentsDbContext.Departments.FirstOrDefaultAsync(d => d.Id == guid);
            return entity != null;
        }
    }
}
