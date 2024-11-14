

namespace Patients.Patients.Features.GetMedicalRecorsByPatient
{
    public record GetMedicalRecordsByPatientQuery(Guid PatientId ) : IRequest<GetMedicalRecordsByPatientQueryResponse>;
    public record GetMedicalRecordsByPatientQueryResponse(List<MedicalRecordDto> MedicalRecordDtos);
    public class GetMedicalRecordsByPatientQueryHandler(PatientsDbContext patientsDbContext) : IRequestHandler<GetMedicalRecordsByPatientQuery, GetMedicalRecordsByPatientQueryResponse>
    {
        public async Task<GetMedicalRecordsByPatientQueryResponse> Handle(GetMedicalRecordsByPatientQuery request, CancellationToken cancellationToken)
        {
            var entity = await patientsDbContext.Patients.FirstOrDefaultAsync(s => s.Id == request.PatientId);
            if (entity == null)
            {
                throw new NotFoundException($"Patinet with id {request.PatientId} was not found");
            }

            var records  = await patientsDbContext.MedicalRecords
                .Where(s=>s.PatientId == request.PatientId)
                .ToListAsync(cancellationToken);

            var mapped= records.Adapt<List<MedicalRecordDto>>();

            return new GetMedicalRecordsByPatientQueryResponse(mapped);
        }
    }
}
