
using Patients.Patients.Entities;

namespace Patients.Patients.Features.AddAllegyToPatient
{
    public record AddAllegyToPatientCommand(Guid PatientId,AllergyDto AllergyDto ) : IRequest<AddAllegyToPatientCommandResponse>;
    public record AddAllegyToPatientCommandResponse(bool Succeded);
    public class AddAllegyToPatientCommandHandler(PatientsDbContext patientsDbContext, ISender sender) : IRequestHandler<AddAllegyToPatientCommand, AddAllegyToPatientCommandResponse>
    {
        public async Task<AddAllegyToPatientCommandResponse> Handle(AddAllegyToPatientCommand request, CancellationToken cancellationToken)
        {
            var patient = await patientsDbContext.Patients
                .Include(s=>s.Allergies)
                .FirstOrDefaultAsync(s=>s.Id == request.PatientId);

            if (patient == null)
            {
                throw new NotFoundException($"Patient with id {request.PatientId} was not found");
            }

            AddAllergy(patient, request.AllergyDto);
             patientsDbContext.Patients.Update(patient);
            await patientsDbContext.SaveChangesAsync(cancellationToken);
            return new AddAllegyToPatientCommandResponse(true);

        }

        private  void AddAllergy(Patient patient, AllergyDto allergyDto)
        {
            Patient.AddAllergy(allergyDto.AllergyName, allergyDto.AllergyType, allergyDto.Reaction, DateTime.UtcNow);
        }
    }
}
