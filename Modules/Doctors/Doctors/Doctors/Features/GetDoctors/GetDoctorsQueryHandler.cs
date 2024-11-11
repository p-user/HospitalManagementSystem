using Doctors.Contracts.Doctors.Dtos;
using Shared.Pagination;

namespace Doctors.Doctors.Features.GetDoctors
{
    public record GetDoctorsQuery(PaginationRequest PaginationRequest) :IRequest<GetDoctorsQueryResponse>;
    public record GetDoctorsQueryResponse(PaginatedResult<DoctorDto> DoctorDtos);
    public class GetDoctorsQueryHandler(DoctorsDbContext doctorsDbContext) : IRequestHandler<GetDoctorsQuery, GetDoctorsQueryResponse>
    {
        public async Task<GetDoctorsQueryResponse> Handle(GetDoctorsQuery request, CancellationToken cancellationToken)
        {

            var pageIndex = request.PaginationRequest.PageIndex;
            var pageSize = request.PaginationRequest.PageSize;

            var count = await doctorsDbContext.Doctors.LongCountAsync(cancellationToken);


            var entities = await doctorsDbContext.Doctors
                .AsNoTracking()
                .OrderBy(s=>s.Name)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            var mappedEntities = entities.Adapt<List<DoctorDto>>(); 

            return new GetDoctorsQueryResponse( 
                new PaginatedResult<DoctorDto>(pageIndex, pageSize, count, mappedEntities)
                );
            
        }

        
    }
}
