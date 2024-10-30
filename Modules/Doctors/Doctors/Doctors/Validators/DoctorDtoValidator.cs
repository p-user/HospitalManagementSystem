using Doctors.Data;
using FluentValidation;

namespace Doctors.Doctors.Validators
{
    public class DoctorDtoValidator : AbstractValidator<DoctorDto>
    {
        private readonly DoctorsDbContext _dbContext;
        public DoctorDtoValidator(DoctorsDbContext doctorsDbContext)
        {
            _dbContext = doctorsDbContext;
            RuleFor(x => x.Name).NotEmpty().WithMessage($"{nameof(DoctorDto.Name)} must not be empty");
            RuleFor(x => x.GraduatedUniversity).NotEmpty().WithMessage($"{nameof(DoctorDto.GraduatedUniversity)} must not be empty");
            RuleFor(x => x.DepartmentId).NotEmpty().WithMessage($"{nameof(DoctorDto.DepartmentId)} must not be empty");
            RuleFor(x => x.Specialization).NotEmpty().WithMessage($"{nameof(DoctorDto.Specialization)} must not be empty");
            RuleFor(x => x.Surname).NotEmpty().WithMessage($"{nameof(DoctorDto.Surname)} must not be empty");

            RuleFor(s => s.Specialization).MustAsync(CheckIfExits).WithMessage("Specialization not registered!");
        }

        private async Task<bool> CheckIfExits(Guid guid, CancellationToken token)
        {
            var specialization = await _dbContext.Specializations.FirstOrDefaultAsync(s=>s.Id == guid);
            return specialization != null;
        }
    }
}
