

using Doctors.Data;
using Doctors.Doctors.Dtos;
using MediatR;

namespace Doctors.Doctors.Features.CreateDoctor
{

    public record CreateDoctorCommand(DoctorDto DoctorDto  ) : IRequest<CreateDoctorResult>;


    public record CreateDoctorResult(Guid Id);


    public class CreateDoctorCommandHandler(DoctorsDbContext _dbContext) : IRequestHandler<CreateDoctorCommand, CreateDoctorResult>
    {
        public async Task<CreateDoctorResult> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
        {
           //TODO : inject repository pattern  in class INSTEAD OF dbcontext

            //create entity from dto 
            var doctorEntity = CreateNewDoctor(request.DoctorDto);

            //TODO : add & save using repository pattern
             _dbContext.Doctors.Add(doctorEntity);
            await _dbContext.SaveChangesAsync();

            //return obj
            return new CreateDoctorResult(doctorEntity.Id);

        }

        private Doctor CreateNewDoctor(DoctorDto DoctorDto) 
        {
            var doctor =  Doctor.Create(DoctorDto);
            return doctor;
                
        }
    }
}
