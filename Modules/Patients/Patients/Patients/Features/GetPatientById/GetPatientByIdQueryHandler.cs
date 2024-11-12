

namespace Patients.Patients.Features.GetPatientById
{
    public record GetPatientByIdQuery(Guid PatientId) : IRequest<GetPatientByIdQueryResponse>;
    public record GetPatientByIdQueryResponse(PatientDto PatientDto);
    internal class GetPatientByIdQueryHandler(PatientsDbContext patientsDbContext) : IRequestHandler<GetPatientByIdQuery, GetPatientByIdQueryResponse>
    {
        public async Task<GetPatientByIdQueryResponse> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await patientsDbContext.Patients.FirstOrDefaultAsync(s=>s.Id == request.PatientId);
            if (entity == null) 
            {
                throw new NotFoundException($"Patinet with id {request.PatientId} was not found");
            }

            var response =  entity.Adapt<PatientDto>();
            return new GetPatientByIdQueryResponse(response);
        }
    }
}
