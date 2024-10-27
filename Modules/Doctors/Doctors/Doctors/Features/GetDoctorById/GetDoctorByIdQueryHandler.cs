using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctors.Doctors.Features.GetDoctorById
{
    public record GetDoctorByIdQuery(Guid Id) :IRequest<GetDoctorByIdQueryResponse>;
    public record GetDoctorByIdQueryResponse(DoctorDto Entity);
    public class GetDoctorByIdQueryHandler(DoctorsDbContext doctorsDbContext) : IRequestHandler<GetDoctorByIdQuery, GetDoctorByIdQueryResponse>
    {
        public async  Task<GetDoctorByIdQueryResponse> Handle(GetDoctorByIdQuery request, CancellationToken cancellationToken)
        {
            var doctor = await doctorsDbContext.Doctors.AsNoTracking().SingleOrDefaultAsync(s=>s.Id==request.Id, cancellationToken);
            if (doctor is null) { throw new Exception($"Doctor not found to update!"); }

            var mappedEntity = doctor.Adapt<DoctorDto>();
            return new GetDoctorByIdQueryResponse(mappedEntity);
        }
    }
}
