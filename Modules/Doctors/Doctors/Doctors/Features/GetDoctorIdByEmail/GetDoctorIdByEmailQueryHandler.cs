
using Doctors.Contracts.Doctors.Features.GetDoctorIdByEmail;
using Shared.Exceptions;

namespace Doctors.Doctors.Features.GetDoctorIdByEmail
{
    public class GetDoctorIdByEmailQueryHandler(DoctorsDbContext doctorsDbContext) : IRequestHandler<GetDoctorIdByEmailQuery, GetDoctorIdByEmailQueryResponse>
    {
        public async Task<GetDoctorIdByEmailQueryResponse> Handle(GetDoctorIdByEmailQuery request, CancellationToken cancellationToken)
        {
            var doctor = await doctorsDbContext.Doctors.Where(s=>s.Email == request.Email).FirstOrDefaultAsync(cancellationToken);

            if (doctor is null)
            {
                throw new NotFoundException($"Doctor was not found with email {request.Email}");
            }
            return new GetDoctorIdByEmailQueryResponse(doctor.Id.ToString());
        }
    }
}
