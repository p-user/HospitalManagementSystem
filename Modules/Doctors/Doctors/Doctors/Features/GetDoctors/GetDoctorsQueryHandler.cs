namespace Doctors.Doctors.Features.GetDoctors
{
    public record GetDoctorsQuery() :IRequest<GetDoctorsQueryResponse>;
    public record GetDoctorsQueryResponse(List<DoctorDto> DoctorDtos);
    public class GetDoctorsQueryHandler(DoctorsDbContext doctorsDbContext) : IRequestHandler<GetDoctorsQuery, GetDoctorsQueryResponse>
    {
        public async Task<GetDoctorsQueryResponse> Handle(GetDoctorsQuery request, CancellationToken cancellationToken)
        {
            var entities = await doctorsDbContext.Doctors.AsNoTracking().ToListAsync();
            var mappedEntities = entities.Adapt<List<DoctorDto>>(); 

            return new GetDoctorsQueryResponse(mappedEntities);
            
        }

        
    }
}
