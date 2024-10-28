

using Doctors.Doctors.Features.GetDoctors;
using Shared.Pagination;

namespace Doctors.Doctors.Features.GetSpecializations
{
    public record GetSpecializationsQuery(): IRequest<GetSpecializationsQueryResponse>;
    public record GetSpecializationsQueryResponse(List<SpecializationDto> SpecializationDtos);
    public class GetSpecializationsQueryHandler(DoctorsDbContext doctorsDbContext) : IRequestHandler<GetSpecializationsQuery, GetSpecializationsQueryResponse>
    {
        public async Task<GetSpecializationsQueryResponse> Handle(GetSpecializationsQuery request, CancellationToken cancellationToken)
        {
           
            var entities = await doctorsDbContext.Specializations
                .AsNoTracking()
                .OrderBy(s => s.Name)
                .ToListAsync(cancellationToken);

            var mappedEntities = entities.Adapt<List<SpecializationDto>>();

            return new GetSpecializationsQueryResponse(mappedEntities);
                
        }
    }
}
