

namespace Appointments.Appointments.Features.UpdateAvailableSlot
{
    public record UpdateAvailableSlotCommand(Guid Id, DateTime startTime, DateTime endTime, int appoinmentLimit):IRequest<UpdateAvailableSlotCommandResponse>;
    public record UpdateAvailableSlotCommandResponse(bool Succeded);
    public class UpdateAvailableSlotCommandHandler(AppointmentsDbContext appointmentsDbContext) : IRequestHandler<UpdateAvailableSlotCommand, UpdateAvailableSlotCommandResponse>
    {
        public async Task<UpdateAvailableSlotCommandResponse> Handle(UpdateAvailableSlotCommand request, CancellationToken cancellationToken)
        {
            var entity = await appointmentsDbContext.AvailableSlots.FindAsync(request.Id);

            if (entity == null)
            { 
                throw new NotFoundException($"Available slot was not found with id {request.Id}");
            }

            UpdateAvailableSlot(request.appoinmentLimit, request.startTime, request.endTime, entity);
            appointmentsDbContext.AvailableSlots.Update(entity);
            await appointmentsDbContext.SaveChangesAsync(cancellationToken);

            return new UpdateAvailableSlotCommandResponse(true);

        }

        private void UpdateAvailableSlot(int appoinmentLimit, DateTime startTime, DateTime endTime, AvailableSlot entity)
        {
            entity.Update(startTime, endTime, appoinmentLimit);
            entity.CheckBookingLimit();
        }
    }
}
