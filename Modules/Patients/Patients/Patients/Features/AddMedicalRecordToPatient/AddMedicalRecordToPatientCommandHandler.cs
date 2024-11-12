
using Patients.Patients.Entities;

namespace Patients.Patients.Features.AddMedicalRecordToPatient
{
    public record AddMedicalRecordToPatientCommand(MedicalRecordDto MedicalRecord) : IRequest<AddMedicalRecordToPatientCommandResponse>;
    public record AddMedicalRecordToPatientCommandResponse(bool Succeded);
    public class AddMedicalRecordToPatientCommandHandler(PatientsDbContext patientsDbContext) : IRequestHandler<AddMedicalRecordToPatientCommand, AddMedicalRecordToPatientCommandResponse>
    {
        public async Task<AddMedicalRecordToPatientCommandResponse> Handle(AddMedicalRecordToPatientCommand request, CancellationToken cancellationToken)
        {
            var patient = await patientsDbContext.Patients
                .Include(s=>s.MedicalRecords)
                .FirstOrDefaultAsync(s => s.Id == request.MedicalRecord.PatientId);

            if (patient == null)
            {
                throw new NotFoundException($"Patient with id {request.MedicalRecord.PatientId} was not found");
            }

            AddMedicalRecord(patient, request.MedicalRecord);
            patientsDbContext.Patients.Update(patient);
            await patientsDbContext.SaveChangesAsync(cancellationToken);
            return new AddMedicalRecordToPatientCommandResponse(true);
        }

        private void AddMedicalRecord(Patient patient, MedicalRecordDto medicalRecord)
        {
            patient.AddMedicalRecord(medicalRecord.Title, medicalRecord.Description, medicalRecord.RecordType);
        }
    }
}
