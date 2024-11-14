

namespace Patients.Patients.Features.GetCurrentUsersPatientId
{

    public record GetCurrentUsersPatientIdQuery(string email) : IRequest<string>;
    public class GetCurrentUsersPatientIdQueryHandler(PatientsDbContext patientsDbContext) : IRequestHandler<GetCurrentUsersPatientIdQuery, string>
    {
        public async  Task<string> Handle(GetCurrentUsersPatientIdQuery request, CancellationToken cancellationToken)
        {
             return await patientsDbContext.Patients.Where(s=>s.Email == request.email).Select(s=>s.Id.ToString()).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
