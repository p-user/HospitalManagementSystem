
using Patients.Patients.Entities;

namespace Patients.Patients.Features.RemoveAllergyFromPatient

{
    public record RemoveAllergyFromPatientCommand(Guid PatientId,string AllergyName ) : IRequest<RemoveAllergyFromPatientCommandResponse>;
    public record RemoveAllergyFromPatientCommandResponse(bool Succeded);
    public class RemoveAllergyFromPatientCommandHandler(PatientsDbContext patientsDbContext, ISender sender) : IRequestHandler<RemoveAllergyFromPatientCommand, RemoveAllergyFromPatientCommandResponse>
    {
        public async Task<RemoveAllergyFromPatientCommandResponse> Handle(RemoveAllergyFromPatientCommand request, CancellationToken cancellationToken)
        {
            var patient = await patientsDbContext.Patients
                .Include(p=>p.Allergies)
                .FirstOrDefaultAsync(s=>s.Id == request.PatientId);


            if (patient == null)
            {
                throw new NotFoundException($"Patient with id {request.PatientId} was not found");
            }

            var allergyToDelete = patient.Allergies
            .FirstOrDefault(a => a.AllergyName == request.AllergyName);

            if (allergyToDelete == null)
            {
                throw new NotFoundException($"Allergy  with name  {request.AllergyName} was not found associated with this patient");
            }

            RemoveAllergy(patient, allergyToDelete);
             patientsDbContext.Patients.Update(patient);
            await patientsDbContext.SaveChangesAsync(cancellationToken);
            return new RemoveAllergyFromPatientCommandResponse(true);

        }

        private  void RemoveAllergy(Patient patient, Allergy allergyDto)
        {
            Patient.RemoveAllergy(allergyDto);
        }
    }
}
