﻿using Doctors.Data;
using Doctors.Doctors.Dtos;
using FluentValidation;
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
    public class UpdateDoctorCommandHandler(DoctorsDbContext doctorsDbContext, IValidator<DoctorDto> validator) : IRequestHandler<UpdateDoctorCommand, UpdateDoctorCommandResponse>
    {
        public async Task<UpdateDoctorCommandResponse> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
        {
            //ToDo : implement the repository pattern

            var validationResult = await validator.ValidateAsync(request.DoctorDto, cancellationToken);
            if (validationResult.Errors.Any())
            {
                throw new ValidationException(validationResult.Errors.Select(s => s.ErrorMessage).FirstOrDefault());

            }

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
