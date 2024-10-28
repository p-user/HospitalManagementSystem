
namespace Doctors.Doctors.Features.CreateSpecialization
{

    public record CreateSpecializationCommand(SpecializationDto SpecializationDto) : IRequest<CreateSpecializationCommandResponse>;
    public record CreateSpecializationCommandResponse(Guid Id);
    public class CreateSpecializationCommandHandler(DoctorsDbContext doctorsDbContext) : IRequestHandler<CreateSpecializationCommand, CreateSpecializationCommandResponse>
    {
        public async Task<CreateSpecializationCommandResponse> Handle(CreateSpecializationCommand request, CancellationToken cancellationToken)
        {
            //validate incoming request


            //create % save
            var entity = CreateSpecialization(request.SpecializationDto);
            await doctorsDbContext.Specializations.AddAsync(entity, cancellationToken);
            await doctorsDbContext.SaveChangesAsync(cancellationToken);

            //response
            return new CreateSpecializationCommandResponse(entity.Id);
        }

        private Specialization CreateSpecialization(SpecializationDto specializationDto)
        {
            var entity = Specialization.CreateSpecialization(specializationDto.Name, specializationDto.Description);
            return entity;
        }
    }
}
