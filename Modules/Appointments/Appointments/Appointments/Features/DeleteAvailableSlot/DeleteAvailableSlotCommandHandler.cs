

using Microsoft.EntityFrameworkCore;

namespace Appointments.Appointments.Features.DeleteAvailableSlot
{

    public record DeleteAvailableSlotCommand(Guid Id, Guid DoctorId) : IRequest<DeleteAvailableSlotCommandResponse>;
    public record DeleteAvailableSlotCommandResponse(bool Succeded);
    public class DeleteAvailableSlotCommandHandler(AppointmentsDbContext appointmentsDbContext) : IRequestHandler<DeleteAvailableSlotCommand, DeleteAvailableSlotCommandResponse>
    {
        public async  Task<DeleteAvailableSlotCommandResponse> Handle(DeleteAvailableSlotCommand request, CancellationToken cancellationToken)
        {
            var entity = await appointmentsDbContext.AvailableSlots
                .Include(s=>s.Appointments)
                .Where(s=>s.Id==request.Id)
                .FirstOrDefaultAsync(cancellationToken);


            if (entity == null) { throw new NotFoundException($" Available slot was not found with Id {request.Id}"); }

            if (entity.DoctorId != request.DoctorId) { throw new Exception($" You are nott authorized for this slot"); }

            if (entity.Appointments.Any()) { throw new Exception($" You have appoinments to arrange in the  slot"); }

            appointmentsDbContext.AvailableSlots.Remove(entity);
            await appointmentsDbContext.SaveChangesAsync(cancellationToken);

            return new DeleteAvailableSlotCommandResponse(true);


        }
    }
}
