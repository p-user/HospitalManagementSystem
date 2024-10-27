using Doctors.Data;
using MediatR;


namespace Doctors.Doctors.Features.DeleteDoctor
{
    public record DeleteDoctorCommand(Guid Id):IRequest<DeleteDoctorCommandResponse>;
    public record DeleteDoctorCommandResponse(bool Succeeded);

    public class DeleteDoctorCommandHandler(DoctorsDbContext  doctorsDbContext) : IRequestHandler<DeleteDoctorCommand, DeleteDoctorCommandResponse>
    {
        public async Task<DeleteDoctorCommandResponse> Handle(DeleteDoctorCommand request, CancellationToken cancellationToken)
        {
            //check if exists
            var doctor = await doctorsDbContext.Doctors.FindAsync(request.Id, cancellationToken);
            if (doctor is null) { throw new Exception($"Doctor not found to delete!"); }

           //Remove
            doctorsDbContext.Doctors.Remove(doctor);
            var rows =await doctorsDbContext.SaveChangesAsync(cancellationToken);
            return new DeleteDoctorCommandResponse(rows>0);
            
        }
    }
}
