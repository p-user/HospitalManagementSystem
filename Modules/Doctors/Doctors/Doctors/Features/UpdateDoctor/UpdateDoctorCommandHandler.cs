using Doctors.Data;
using Doctors.Doctors.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctors.Doctors.Features.UpdateDoctor
{

    public record UpdateDoctorCommand (DoctorDto DoctorDto, Guid Id) : IRequest<UpdateDoctorCommandResponse>;
    public record UpdateDoctorCommandResponse(bool Succeeded);
    public class UpdateDoctorCommandHandler(DoctorsDbContext doctorsDbContext) : IRequestHandler<UpdateDoctorCommand, UpdateDoctorCommandResponse>
    {
        public async Task<UpdateDoctorCommandResponse> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
        {
            //ToDo : implement the repository pattern

            //check if exists

            var doctor  = await doctorsDbContext.Doctors.FindAsync(request.Id, cancellationToken);
            if (doctor is null) { throw new Exception($"Doctor not found to update!"); }

            //update the doctor object 
            UpdateEntityValues(doctor , request.DoctorDto);

            //update & save changes
            doctorsDbContext.Doctors.Update(doctor);
            await doctorsDbContext.SaveChangesAsync(cancellationToken);


                //return resposne
                return new UpdateDoctorCommandResponse(true);
        }

        private void UpdateEntityValues(Doctor doctor, DoctorDto doctorDto)
        {
            doctor.Update(doctorDto);
        }
    }
}
