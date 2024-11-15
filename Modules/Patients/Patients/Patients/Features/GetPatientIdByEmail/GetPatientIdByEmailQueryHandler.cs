

namespace Patients.Patients.Features.GetCurrentUsersPatientId
{

 
    public class GetPatientIdByEmailQueryHandler(PatientsDbContext patientsDbContext) : IRequestHandler<GetPatientIdByEmailQuery, string>
    {
        public async  Task<string> Handle(GetPatientIdByEmailQuery request, CancellationToken cancellationToken)
        {
             return await patientsDbContext.Patients.Where(s=>s.Email == request.email).Select(s=>s.Id.ToString()).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
