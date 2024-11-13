using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Appointments.Appointments.Features.GetAvailableSlotsOfDoctor
{
    public record GetAvailableSlotsOfDoctorQuery(Guid DoctorId) : IRequest<GetAvailableSlotsOfDoctorQueryResponse>;

    public record GetAvailableSlotsOfDoctorQueryResponse(List<AvailableSlotDto> AvailableSlotDtos);
    public class GetAvailableSlotsOfDoctorQueryHandler(AppointmentsDbContext appointmentsDbContext)
                                        : IRequestHandler<GetAvailableSlotsOfDoctorQuery, GetAvailableSlotsOfDoctorQueryResponse>
    {
        public async Task<GetAvailableSlotsOfDoctorQueryResponse> Handle(GetAvailableSlotsOfDoctorQuery request, CancellationToken cancellationToken)
        {
            var entities =await appointmentsDbContext.AvailableSlots.Where(s=>s.DoctorId == request.DoctorId).ToListAsync(cancellationToken);

            var dtos = entities.Adapt<List<AvailableSlotDto>>();

            return new GetAvailableSlotsOfDoctorQueryResponse(dtos);
        }
    }
}
