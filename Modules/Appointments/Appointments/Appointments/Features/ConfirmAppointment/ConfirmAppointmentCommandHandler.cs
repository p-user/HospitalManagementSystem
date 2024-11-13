


using Microsoft.EntityFrameworkCore;

namespace Appointments.Appointments.Features.ConfirmAppointment
{
    public record ConfirmAppointmentCommand(Guid AppoinmentId) : IRequest<ConfirmAppointmentCommandResponse>;
    public record ConfirmAppointmentCommandResponse(bool Succeded);
    public class ConfirmAppointmentCommandHandler(AppointmentsDbContext appointmentsDbContext) : IRequestHandler<ConfirmAppointmentCommand, ConfirmAppointmentCommandResponse>
    {
        public async Task<ConfirmAppointmentCommandResponse> Handle(ConfirmAppointmentCommand request, CancellationToken cancellationToken)
        {
            var entity = await appointmentsDbContext.AvailableSlots
                .Include(s => s.Appointments.Where(s=> s.Id== request.AppoinmentId))
                .FirstOrDefaultAsync(cancellationToken);


            if (entity == null) { throw new NotFoundException($" Available slot was not found with appoinment  {request.AppoinmentId}"); }

            entity.ConfirmAppoinment(request.AppoinmentId);
            entity.CheckBookingLimit();

            appointmentsDbContext.AvailableSlots.Update(entity);
            await appointmentsDbContext.SaveChangesAsync(cancellationToken);

            return new ConfirmAppointmentCommandResponse(true);
        }
    }
}
