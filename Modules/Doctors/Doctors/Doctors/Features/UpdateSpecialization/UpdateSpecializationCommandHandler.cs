

using Doctors.Doctors.Validators;
using Shared.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace Doctors.Doctors.Features.UpdateSpecialization
{

    public record UpdateSpecializationCommand(SpecializationDto SpecializationDto , Guid Id) : IRequest<UpdateSpecializationCommandResponse>;
    public record UpdateSpecializationCommandResponse(Guid Id);
    public class UpdateSpecializationCommandHandler(DoctorsDbContext doctorsDbContext, SpecializationDtoValidators validator ) : IRequestHandler<UpdateSpecializationCommand, UpdateSpecializationCommandResponse>
    {
        public async Task<UpdateSpecializationCommandResponse> Handle(UpdateSpecializationCommand request, CancellationToken cancellationToken)
        {
            //validate 
            var validationResult = await validator.ValidateAsync(request.SpecializationDto, cancellationToken);
            if (validationResult.Errors.Any())
            {
                throw new ValidationException(validationResult.Errors.Select(s => s.ErrorMessage).FirstOrDefault());

            }

            // check ekzistence of entity
            var entity = await doctorsDbContext.Specializations.FirstOrDefaultAsync(s=>s.Id == request.Id);
            if (entity is null)
            {
                throw new NotFoundException($"Specialization with {request.Id.ToString()} not found" );
            }
             UpdateSpecialization(entity,request.SpecializationDto);
            doctorsDbContext.Specializations.Update(entity);
            await doctorsDbContext.SaveChangesAsync(cancellationToken);
            return new UpdateSpecializationCommandResponse(entity.Id);

        }

        private void  UpdateSpecialization(Specialization obj, SpecializationDto specializationDto)
        {
             obj.Update(specializationDto.Name, specializationDto.Description);
        }
    }
}
