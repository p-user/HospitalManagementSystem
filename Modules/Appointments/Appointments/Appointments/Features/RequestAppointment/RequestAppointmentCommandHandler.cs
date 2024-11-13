

using Microsoft.EntityFrameworkCore;

namespace Appointments.Appointments.Features.RequestAppointment
{

    public record RequestAppointmentCommand(Guid SlotId, Guid PatientId) : IRequest<RequestAppointmentCommandResponse>;
    public record RequestAppointmentCommandResponse(bool Succeded);
    public class RequestAppointmentCommandHandler(AppointmentsDbContext appointmentsDbContext) : IRequestHandler<RequestAppointmentCommand, RequestAppointmentCommandResponse>
    {
        public async Task<RequestAppointmentCommandResponse> Handle(RequestAppointmentCommand request, CancellationToken cancellationToken)
        {
            var entity = await appointmentsDbContext.AvailableSlots
                .Include(s=>s.Appointments)
                .Where(s=>s.Id==request.SlotId)
                .FirstOrDefaultAsync(cancellationToken);


            if (entity == null) { throw new NotFoundException($" Available slot was not found with Id {request.SlotId}"); }

            entity.AddAppoinment(request.PatientId);
            entity.CheckBookingLimit();

            appointmentsDbContext.AvailableSlots.Update(entity);
            await appointmentsDbContext.SaveChangesAsync(cancellationToken);

            return new RequestAppointmentCommandResponse(true);

        }
    }
}
