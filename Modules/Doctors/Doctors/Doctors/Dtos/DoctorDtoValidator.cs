
using FluentValidation;

namespace Doctors.Doctors.Dtos
{
    public class DoctorDtoValidator : AbstractValidator<DoctorDto>
    {
        public DoctorDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage($"{nameof(DoctorDto.Name)} must not be empty");
            RuleFor(x => x.GraduatedUniversity).NotEmpty().WithMessage($"{nameof(DoctorDto.GraduatedUniversity)} must not be empty");
            RuleFor(x => x.Department).NotEmpty().WithMessage($"{nameof(DoctorDto.Department)} must not be empty");
            RuleFor(x => x.Specialization).NotEmpty().WithMessage($"{nameof(DoctorDto.Specialization)} must not be empty");
            RuleFor(x => x.Surname).NotEmpty().WithMessage($"{nameof(DoctorDto.Surname)} must not be empty");
        }
    }
}
