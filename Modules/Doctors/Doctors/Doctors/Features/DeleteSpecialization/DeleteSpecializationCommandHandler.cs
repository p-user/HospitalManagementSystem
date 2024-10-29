

using Shared.Exceptions;

namespace Doctors.Doctors.Features.DeleteSpecialization
{
    public record DeleteSpecializationCommand(Guid Id) : IRequest<DeleteSpecializationCommandResponse>;
    public record DeleteSpecializationCommandResponse(bool Succeeded);
    public class DeleteSpecializationCommandHandler(DoctorsDbContext doctorsDbContext) : IRequestHandler<DeleteSpecializationCommand, DeleteSpecializationCommandResponse>
    {
        public async Task<DeleteSpecializationCommandResponse> Handle(DeleteSpecializationCommand request, CancellationToken cancellationToken)
        {
            var entity = await doctorsDbContext.Specializations.FirstOrDefaultAsync(s=>s.Id == request.Id);
            if (entity == null)
            {
                throw new NotFoundException($"Specialization with id {request.Id} was not found");
            }
            var doctorsFound = await doctorsDbContext.Doctors.AnyAsync(s=>s.SpecializationId == request.Id);
            if (doctorsFound)
            {
                throw new BadRequestException
                    ("There are doctors registered on this specialization. Please, remove the doctors first, or update the specialization accordingly!");
            }

            doctorsDbContext.Specializations.Remove(entity);
            await doctorsDbContext.SaveChangesAsync(cancellationToken);

            return new DeleteSpecializationCommandResponse(true);
        }
    }
}
