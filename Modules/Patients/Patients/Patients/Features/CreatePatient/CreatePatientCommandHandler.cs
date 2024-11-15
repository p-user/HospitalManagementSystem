
using Patients.Patients.Entities;

namespace Patients.Patients.Features.CreatePatient
{
    public record CreatePatientCommand(PatientDto Patient) : IRequest<CreatePatientCommandResponse>;
    public record CreatePatientCommandResponse(Guid PatientId);
    public class CreatePatientCommandHandler(PatientsDbContext _dbContext) : IRequestHandler<CreatePatientCommand, CreatePatientCommandResponse>
    {
        public async Task<CreatePatientCommandResponse> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            var entityCheck = await _dbContext.Patients.FirstOrDefaultAsync(s => s.Email == request.Patient.Email);
            if (entityCheck != null) 
            {
                throw new Exception($"A patient is already registered with this email");
            }
            var entity = CreateNewPatient(request.Patient);

            _dbContext.Patients.Add(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            //return obj
            return new CreatePatientCommandResponse(entity.Id);
        }

        private Patient CreateNewPatient(PatientDto PatientDto)
        {
            var patient = Patient.Create(PatientDto.FirstName, PatientDto.LastName, PatientDto.DateOfBirth, PatientDto.Gender, PatientDto.Email,PatientDto.PhoneNumber);
            return patient;

        }
    }
}
