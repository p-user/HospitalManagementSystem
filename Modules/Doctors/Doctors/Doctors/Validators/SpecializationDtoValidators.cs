using FluentValidation;

namespace Doctors.Doctors.Validators
{
    public class SpecializationDtoValidators : AbstractValidator<SpecializationDto>
    {
        public SpecializationDtoValidators()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage($"{nameof(SpecializationDto.Name)} must not be empty");
        }
    }
}
