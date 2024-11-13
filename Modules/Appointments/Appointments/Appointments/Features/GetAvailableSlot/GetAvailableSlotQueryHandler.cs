

using Mapster;

namespace Appointments.Appointments.Features.GetAvailableSlot
{
    public record GetAvailableSlotQuery(Guid Id) : IRequest<GetAvailableSlotQueryResponse>;
    public record GetAvailableSlotQueryResponse(AvailableSlotDto dto) ;
    public class GetAvailableSlotQueryHandler(AppointmentsDbContext appointmentsDbContext) : IRequestHandler<GetAvailableSlotQuery, GetAvailableSlotQueryResponse>
    {
        public async Task<GetAvailableSlotQueryResponse> Handle(GetAvailableSlotQuery request, CancellationToken cancellationToken)
        {
           var entity = await appointmentsDbContext.AvailableSlots.FindAsync(request.Id);
            if (entity == null) { throw new NotFoundException($" Available slot was not found with Id {request.Id}"); }

            var mappedDto = entity.Adapt<AvailableSlotDto>();
            return new GetAvailableSlotQueryResponse(mappedDto);
        }
    }
}
