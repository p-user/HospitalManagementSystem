

using Doctors.Contracts.Doctors.Dtos;

namespace Doctors.Contracts.Doctors.Features.CreateDoctor
{
    public record CreateDoctorCommand(DoctorDto DoctorDto  ) : IRequest<CreateDoctorResult>;


    public record CreateDoctorResult(Guid Id);

}


   
    
