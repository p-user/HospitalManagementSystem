

namespace Patients.Patients.Features.GetAllergiesOfPatient
{
    public record GetAllergiesOfPatientQuery(Guid PatientId) : IRequest<GetAllergiesOfPatientQueryResponse>;
    public record GetAllergiesOfPatientQueryResponse(List<AllergyDto> Allergies);
    public class GetAllergiesOfPatientQueryHandler(PatientsDbContext patientsDbContext) : IRequestHandler<GetAllergiesOfPatientQuery, GetAllergiesOfPatientQueryResponse>
    {
        public async Task<GetAllergiesOfPatientQueryResponse> Handle(GetAllergiesOfPatientQuery request, CancellationToken cancellationToken)
        {
            var patient = await patientsDbContext.Patients
                .Include(s=>s.Allergies)
                .Where(s => s.Id == request.PatientId).FirstOrDefaultAsync(cancellationToken);

            if (patient == null) { throw new NotFoundException($"Patient with id {request.PatientId} was not found!"); }

            var allergies = patient.Allergies.Adapt<List<AllergyDto>>();

           return new GetAllergiesOfPatientQueryResponse(allergies);
                 
        }
    }
}
